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

    public IResult SendOtp(string phoneNumber)
    {
        string otp = new Random().Next(100000, 999999).ToString();

        _cache.Set(phoneNumber, otp, TimeSpan.FromMinutes(5));
        _sms.SendSms(phoneNumber, $"Doğrulama kodunuz: {otp}");

        return new SuccessResult("Doğrulama kodu gönderildi.");
    }

    public IResult VerifyAndCreateReservation(VerifyOtpDto dto)
    {
        if (!_cache.TryGetValue(dto.Reservation.TelNo, out string realOtp))
            return new ErrorResult("Kod süresi doldu veya bulunamadı.");

        if (realOtp != dto.OtpCode)
            return new ErrorResult("Kod hatalı!");

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
            Status = 1
        };

        _context.Entry(rez).State = EntityState.Added;
        _context.SaveChanges();

        _cache.Remove(dto.Reservation.TelNo);

        return new SuccessResult("Rezervasyon kaydı başarılı.");
    }
}
