using DataAccess;
using DataAccess.Helper.SMSSERVICE;
using DataAccess.Service.Abstract;
using DataAccess.Service.Concrete;
using Entities.All.DTO;
using Entities.All.Models.Admin;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;


public class ReservationService
{
    private readonly AppDbContext _context;
    private readonly IMemoryCache _cache;
    private readonly ISmsService _sms;

    public ReservationService(AppDbContext context, IMemoryCache cache, ISmsService sms)
    {
        _context = context;
        _cache = cache;
        _sms = sms;
    }

    public async Task<IResult> SendOtpAsync(string phoneNumber)
    {
        // Güvenli OTP üretimi
        var otp = new Random().Next(100000, 999999).ToString();

        // 5 dakika boyunca geçerli olacak şekilde cache'e yaz
        _cache.Set(phoneNumber, otp, TimeSpan.FromMinutes(5));

        // Daha önceki başarısız denemeleri sıfırla
        string failKey = $"otp_fail_{phoneNumber}";
        _cache.Remove(failKey);

        // OTP'yi gönder (WhatsApp veya SMS üzerinden)
        _sms.SendSmsAsync(phoneNumber, $"S.Y VIP Rezervasyon Doğrulama Kodunuz: {otp}");

        return new SuccessResult("Doğrulama kodu gönderildi.");
    }


    public async Task<IResult> VerifyAndCreateReservationAsync(VerifyOtpDto dto)
    {
        var phoneNumber = dto.Reservation.TelNo;

        // 1. OTP var mı?
        if (!_cache.TryGetValue(phoneNumber, out string realOtp))
            return new ErrorResult("Kod süresi doldu veya bulunamadı.");

        // 2. Kod eşleşiyor mu?
        if (!string.Equals(realOtp?.Trim(), dto.OtpCode?.Trim(), StringComparison.Ordinal))
        {
            // Yanlış deneme sayısını al
            string failKey = $"otp_fail_{phoneNumber}";
            int failCount = _cache.GetOrCreate(failKey, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5);
                return 0;
            });

            failCount++;
            _cache.Set(failKey, failCount, TimeSpan.FromMinutes(5));

            if (failCount >= 5)
            {
                _cache.Remove(phoneNumber);
                _cache.Remove(failKey);
                return new ErrorResult("Çok fazla başarısız giriş. Kod sıfırlandı, yeniden isteyiniz.");
            }

            return new ErrorResult($"Kod hatalı! ({failCount}/5)");
        }

        // 3. Kod doğru → rezervasyon kaydı yap
        var rez = new Rezervations
        {
            Name = dto.Reservation.Name,
            Surname = dto.Reservation.Surname,
            TelNo = dto.Reservation.TelNo,
            Email = dto.Reservation.Email,
            StartLocation = dto.Reservation.StartLocation,
            EndLocation = dto.Reservation.EndLocation,
            RezTime = dto.Reservation.RezTime,
            RezDate = dto.Reservation.RezDate,
            Status = 1,
            CreatedTime = DateTime.Now,
        };

        _context.Entry(rez).State = EntityState.Added;
        await _context.SaveChangesAsync();

        // Kod ve yanlış giriş verilerini temizle
        _cache.Remove(phoneNumber);
        _cache.Remove($"otp_fail_{phoneNumber}");

        return new SuccessResult("Rezervasyon kaydı başarılı.");
    }

}
