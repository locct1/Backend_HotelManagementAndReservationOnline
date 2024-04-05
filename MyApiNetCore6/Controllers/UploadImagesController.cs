using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApiNetCore6.Data;
using MyApiNetCore6.Helpers;
using MyApiNetCore6.Models;
using MyApiNetCore6.Models.Image;
using System.Data;

namespace MyApiNetCore6.Controllers
{
    [Route("api/upload-images")]
    [ApiController]
    [Authorize(Roles = "HostHotel")]

    public class UploadImagesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IGetValueToken _getValueToken;


        public UploadImagesController(AppDbContext context, IMapper mapper, IGetValueToken getValueToken)
        {
            _context = context;
            _mapper = mapper;
            _getValueToken = getValueToken;
        }
        //Upload RoomType
        [HttpPost("roomtype/{id}")]
        public async Task<IActionResult> UploadRoomType(int id, [FromForm(Name = "FileUpload")] List<IFormFile> files)
        {


            try
            {
                if (files.Count > 0)
                {
                    string[] departmentArray = { ".png", ".jpeg", ".jpg", ".webp" };
                    foreach (var f in files)
                    {
                        string checkEntentions = Path.GetExtension(f.FileName).ToLower();

                        if (departmentArray.Contains(checkEntentions) == false)
                        {
                            return BadRequest(new Response
                            {
                                Success = false,
                                Message = "File không đúng định dạng",
                            });
                        }
                        var file1 = Path.GetFileNameWithoutExtension(Path.GetRandomFileName())
                    + Path.GetExtension(f.FileName);

                        var file = Path.Combine("Uploads", "RoomTypes", file1);

                        using (var filestream = new FileStream(file, FileMode.Create))
                        {
                            await f.CopyToAsync(filestream);
                        }
                        _context.Add(new RoomTypePhoto()
                        {
                            RoomTypeId = id,
                            FileName = file1
                        });

                        await _context.SaveChangesAsync();

                    }
                    return Ok(new Response
                    {
                        Success = true,
                        Message = "Upload ảnh thành công",
                        Data = files
                    });
                }
                return BadRequest(new Response
                {
                    Success = false,
                    Message = "Không có file",
                });
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                {
                    Success = false,
                    Message = "Internal server error",
                });
                throw;
            }
        }
        [HttpGet("roomtype/{id}")]
        public async Task<IActionResult> GetRoomType(int id)
        {
            try
            {
                var response = await _context.RoomTypePhotos!.Where(i => i.RoomTypeId == id).ToListAsync();
                var data = _mapper.Map<List<RoomTypePhotoModel>>(response);
                return Ok(new Response
                {
                    Success = true,
                    Message = "Thành công",
                    Data = data
                });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                {
                    Success = false,
                    Message = "Internal server error",
                });
                throw;
            }
        }
        [HttpDelete("roomtype/{id}")]
        public async Task<IActionResult> DeleteRoomTypeImage(int id)
        {
            try
            {
                var photo = _context.RoomTypePhotos!.Where(p => p.Id == id).FirstOrDefault();
                if (photo != null)
                {
                    _context.Remove(photo);
                    _context.SaveChanges();

                    var filename = "Uploads/RoomTypes/" + photo.FileName;
                    System.IO.File.Delete(filename);
                    return Ok(new Response
                    {
                        Success = true,
                        Message = "Xóa ảnh thành công",
                    });
                }
                return BadRequest(new Response
                {
                    Success = true,
                    Message = "Xóa thất bại",
                });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                {
                    Success = false,
                    Message = "Internal server error",
                });
                throw;
            }
        }

        //Upload Hotel
        [HttpPost("hotel")]
        public async Task<IActionResult> UploadHotel([FromForm(Name = "FileUpload")] List<IFormFile> files)
        {
            try
            {
                if (files.Count > 0)
                {
                    string[] departmentArray = { ".png", ".jpeg", ".jpg", ".webp" };
                    foreach (var f in files)
                    {
                        string checkEntentions = Path.GetExtension(f.FileName).ToLower();

                        if (departmentArray.Contains(checkEntentions) == false)
                        {
                            return BadRequest(new Response
                            {
                                Success = false,
                                Message = "File không đúng định dạng",
                            });
                        }
                        var file1 = Path.GetFileNameWithoutExtension(Path.GetRandomFileName())
                    + Path.GetExtension(f.FileName);

                        var file = Path.Combine("Uploads", "Hotels", file1);

                        using (var filestream = new FileStream(file, FileMode.Create))
                        {
                            await f.CopyToAsync(filestream);
                        }
                        var hotelId = _getValueToken.GetClaimValue(HttpContext, "HotelId");
                        _context.Add(new HotelPhoto()
                        {
                            HotelId = Int32.Parse(hotelId),
                            FileName = file1
                        });

                        await _context.SaveChangesAsync();

                    }
                    return Ok(new Response
                    {
                        Success = true,
                        Message = "Upload ảnh thành công",
                        Data = files
                    });
                }
                return BadRequest(new Response
                {
                    Success = false,
                    Message = "Không có file",
                });
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                {
                    Success = false,
                    Message = "Internal server error",
                });
                throw;
            }
        }
        [HttpGet("hotel")]
        public async Task<IActionResult> GetHotelImage()
        {
            try
            {
                var hotelId = _getValueToken.GetClaimValue(HttpContext, "HotelId");
                var response = await _context.HotelPhotos!.Where(i => i.HotelId == Int32.Parse(hotelId)).ToListAsync();
                var data = _mapper.Map<List<HotelPhotoModel>>(response);
                return Ok(new Response
                {
                    Success = true,
                    Message = "Thành công",
                    Data = data
                });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                {
                    Success = false,
                    Message = "Internal server error",
                });
                throw;
            }
        }
        [HttpDelete("hotel/{id}")]
        public async Task<IActionResult> DeleteHotelImage(int id)
        {
            try
            {
                var photo = _context.HotelPhotos!.Where(p => p.Id == id).FirstOrDefault();
                if (photo != null)
                {
                    _context.Remove(photo);
                    _context.SaveChanges();

                    var filename = "Uploads/Hotels/" + photo.FileName;
                    System.IO.File.Delete(filename);
                    return Ok(new Response
                    {
                        Success = true,
                        Message = "Xóa ảnh thành công",
                    });
                }
                return BadRequest(new Response
                {
                    Success = true,
                    Message = "Xóa thất bại",
                });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                {
                    Success = false,
                    Message = "Internal server error",
                });
                throw;
            }
        }
        //Upload Hotel-Avatar
        [HttpPost("hotel-avatar")]
        public async Task<IActionResult> UploadHotelAvatar([FromForm(Name = "FileUpload")] IFormFile file)
        {
            try
            {
                if (file.Length > 0)
                {
                    string[] departmentArray = { ".png", ".jpeg", ".jpg", ".webp" };
                    string checkEntentions = Path.GetExtension(file.FileName).ToLower();

                    if (departmentArray.Contains(checkEntentions) == false)
                    {
                        return BadRequest(new Response
                        {
                            Success = false,
                            Message = "File không đúng định dạng",
                        });
                    }
                    var file1 = Path.GetFileNameWithoutExtension(Path.GetRandomFileName())
                + Path.GetExtension(file.FileName);

                    var fileTemp = Path.Combine("Uploads", "Avatar", file1);

                    using (var filestream = new FileStream(fileTemp, FileMode.Create))
                    {
                        await file.CopyToAsync(filestream);
                    }
                    var hotelId = _getValueToken.GetClaimValue(HttpContext, "HotelId");
                    var record = _context.Hotels!.Where(p => p.Id == Int32.Parse(hotelId)).FirstOrDefault();
                    record.FileName = file1;
                    _context.Hotels!.Update(record);
                    await _context.SaveChangesAsync();


                    return Ok(new Response
                    {
                        Success = true,
                        Message = "Upload ảnh thành công",
                        Data = file
                    });
                }
                return BadRequest(new Response
                {
                    Success = false,
                    Message = "Không có file",
                });
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                {
                    Success = false,
                    Message = "Internal server error",
                });
                throw;
            }
        }
        [HttpGet("hotel-avatar")]
        public async Task<IActionResult> GetHotelAvatar()
        {
            try
            {
                var hotelId = _getValueToken.GetClaimValue(HttpContext, "HotelId");
                var photo = _context.Hotels!.Where(p => p.Id == Int32.Parse(hotelId)).FirstOrDefault();
                var data = _mapper.Map<HotelAvatarModel>(photo);
                return Ok(new Response
                {
                    Success = true,
                    Message = "Thành công",
                    Data = data
                });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                {
                    Success = false,
                    Message = "Internal server error",
                });
                throw;
            }
        }
        [HttpDelete("hotel-avatar")]
        public async Task<IActionResult> DeleteHotelAvatar()
        {
            try
            {
                var hotelId = _getValueToken.GetClaimValue(HttpContext, "HotelId");
                var hotel = _context.Hotels!.Where(p => p.Id == Int32.Parse(hotelId)).FirstOrDefault();
                if (hotel != null)
                {
                    var filename = "Uploads/Avatar/" + hotel.FileName;
                    System.IO.File.Delete(filename);
                    hotel.FileName = null;
                    _context.Hotels!.Update(hotel);
                    _context.SaveChanges();
                    return Ok(new Response
                    {
                        Success = true,
                        Message = "Xóa ảnh thành công",
                    });
                }
                return BadRequest(new Response
                {
                    Success = true,
                    Message = "Xóa thất bại",
                });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                {
                    Success = false,
                    Message = "Internal server error",
                });
                throw;
            }
        }
    }
}
