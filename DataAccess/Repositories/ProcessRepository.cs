using DataAccess.Service.Abstract;
using DataAccess.Service.Concrete;
using Entities.All.DTO;
using Entities.All.Models.Admin;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

public class ProcessRepository
{

    private readonly AppDbContext _context;

    public ProcessRepository(AppDbContext context)
    {
        _context = context;
    }

    #region Deneme Process
    public IResult Deneme()
    {
        // Örnek geçici müşteri nesnesi (gerçek uygulamada veritabanına kaydedilir)
        var tempCustomer = new
        {
            Id = Guid.NewGuid(),
            Name = "Temp Customer",
            CreatedAt = DateTime.UtcNow
        };

        // Başarılı sonucu dön
        return new SuccessResult("APİ deneme başarılı.");

    }

    public IResult PostDeneme(DenemeDto dto)
    {
        // Örnek geçici müşteri nesnesi (gerçek uygulamada veritabanına kaydedilir)
        var tempCustomer = new
        {
            Id = Guid.NewGuid(),
            Name = "Temp Customer 2",
            CreatedAt = DateTime.UtcNow
        };
        // Başarılı sonucu dön
        return new SuccessResult("Kayıt başarılı.");
    }

    #endregion

    #region GET Process
    public IResult GUsers() // Sisteme giriş yapabilen kullanıcı kayıt bilgileri
    {
        try
        {
            var Users = _context.Users.FirstOrDefault();

            return new SuccessDataResult<Users?>(Users, "Başarılı.");
        }
        catch (Exception)
        {
            return new ErrorResult("Hata");
        }

    }

    public IResult GReservations() // Tüm rezervasyon Kayıtları için
    {
        try
        {
            var AllRezervations = _context.Rezervations.ToList();

            return new SuccessDataResult<List<Rezervations>>(AllRezervations, "Başarılı.");

        }
        catch
        {
            return new ErrorResult("Hata");
        }
    }

    public IResult GReservationsById(int id) // Rezervayon Detayları İçin
    {
        try
        {
            var founderRez = _context.Rezervations.FirstOrDefault(r => r.Id == id);

            if (founderRez == null)
            {
                return new ErrorResult("Kullanıcı adı veya şifre hatalı.");
            }

            return new SuccessDataResult<Rezervations>(founderRez, "Deneme başarılı.");
        }
        catch
        {
            return new ErrorResult("Hata");
        }

    }

    public IDataResult<List<GetPhotoDto>> GAllPhotos()
    {
        try
        {
            var photos = _context.Photos
                .OrderByDescending(p => p.CreatedAt)
                .Select(p => new GetPhotoDto
                {
                    Id = p.Id,
                    Title = p.Title,
                    Description = p.Description,
                    FilePath = p.FilePath,
                    CreatedAt = p.CreatedAt
                })
                .ToList();

            return new SuccessDataResult<List<GetPhotoDto>>(photos, "Fotoğraflar başarıyla getirildi.");
        }
        catch (Exception ex)
        {
            return new ErrorDataResult<List<GetPhotoDto>>(null, "Hata: " + ex.Message);
        }
    }

    #endregion

    #region OTP Process

    public IResult SendOtp(string phoneNumber)
    {
        string otp = new Random().Next(100000, 999999).ToString();

        // OTP'yi bir yere kaydet (örnek: MemoryCache)
        //_memoryCache.Set(phoneNumber, otp, TimeSpan.FromMinutes(5));

        // SMS gönderme işlemi (SMS servisine göre değişir)
        //SmsService.Send(phoneNumber, $"Rezervasyon kodunuz: {otp}");

        return new SuccessResult("Doğrulama kodu gönderildi.");
    }

    #endregion

    #region POST Process
    public IResult PReservation(SaveReservationDto dto) // Rezervasyon oluşturma işlemi için
    {
        try
        {
            Rezervations reg = new Rezervations
            {
                Name = dto.Name,
                Surname = dto.Surname,
                TelNo = dto.TelNo,
                RezDate = dto.RezDate,
                RezTime = dto.RezTime,
                StartLocation = dto.StartLocation,
                EndLocation = dto.EndLocation,
                Email = dto.Email,
                Status = 1
            };

            _context.Entry(reg).State = EntityState.Added;
            _context.SaveChanges();

            return new SuccessResult("Rezervasyon kaydı başarılı.");
        }
        catch
        {
            return new ErrorResult("Rezervasyon Kaydı Esnasında Hata!");
        } 
    }

    public IResult PSavePhoto(SavePhotoDto dto)
    {
        try
        {
            if (dto.PhotoFile == null || dto.PhotoFile.Length == 0)
                return new ErrorResult("Fotoğraf dosyası boş.");

            // uploads klasörünü oluştur
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            // Benzersiz dosya adı
            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(dto.PhotoFile.FileName);
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            // Fiziksel olarak kaydet
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                dto.PhotoFile.CopyTo(stream);
            }

            // Veritabanına kaydedilecek kayıt
            var photo = new Photos
            {
                Title = dto.Title,
                Description = dto.Description,
                FilePath = "/uploads/" + uniqueFileName  // Web üzerinden erişilebilir yol
            };

            _context.Photos.Add(photo);  // EF DbContext
            _context.SaveChanges();

            return new SuccessResult("Fotoğraf başarıyla kaydedildi.");
        }
        catch (Exception ex)
        {
            return new ErrorResult("Hata oluştu: " + ex.Message);
        }
    }

    #endregion
}
