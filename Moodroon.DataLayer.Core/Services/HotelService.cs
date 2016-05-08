namespace Moodroon.DataLayer.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using DataAccessLayer.Models.Models;
    using DataAccessLayer.Repositories.Repositories.DataBaseRepositories;

    using Microsoft.Practices.Unity;

    using Moodroon.DataLayer.Core.DTO;

    public class HotelService : BaseApplicationService
    {
        public HotelService(IUnityContainer container)
            : base(container)
        {
        }

        public List<HotelDto> GetAllHotels()
        {
            try
            {
                return this.InvokeInUnitOfWorkScope(
                    uow =>
                    {
                        var repo = uow.GetRepository<HotelRepository>();
                        var hotels = repo.Get().ToList().Select(p => new HotelDto
                        {
                            Id = p.Id,
                            Images = p.Images.Select(x => x.Url).ToList(),
                            Adress = p.Adress,
                            Name = p.Name,
                            Description = p.Description,
                            Stars = p.Stars,
                            Rooms = p.Rooms.Select(z => new RoomDto
                            {
                                Id = z.Id,
                                Description = z.Description,
                                Number = z.Number,
                                PersonCount = z.PersonCount,
                                Images = z.Images.Select(x => x.Url).ToList(),
                                Price = z.Price,
                                HotelName = z.Hotel.Name
                            }).ToList(),
                            Conditions = p.Conditionses.Select(y => new ConditionDto
                            {
                                Id = y.Id,
                                Name = y.Name,
                                Images = y.Images.Select(x => x.Url).ToList()
                            }).ToList()
                        });
                        return hotels.ToList();
                    });
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public void DeleteHotel(int hotelId)
        {
            this.InvokeInUnitOfWorkScope(
                uow =>
                {
                    var repo = uow.GetRepository<HotelRepository>();
                    var deleteHotel = repo.GetSingle(p => p.Id == hotelId);
                    repo.Remove(deleteHotel);
                    uow.SaveChanges();
                });
        }



        public HotelDto GetHotel(int hotelId)
        {
            return this.InvokeInUnitOfWorkScope(
                uow =>
                {
                    var repo = uow.GetRepository<HotelRepository>();
                    var hotel = repo.GetSingle(p => p.Id == hotelId);
                    return new HotelDto
                    {
                        Adress = hotel.Adress,
                        Id = hotel.Id,
                        Description = hotel.Description,
                        Name = hotel.Name,
                        Stars = hotel.Stars,
                        Images = hotel.Images.Select(p => p.Url).ToList(),
                        Rooms = hotel.Rooms.Select(z => new RoomDto
                        {
                            Id = z.Id,
                            Description = z.Description,
                            Number = z.Number,
                            PersonCount = z.PersonCount,
                            Images = z.Images.Select(x => x.Url).ToList(),
                            Price = z.Price,
                            HotelName = z.Hotel.Name
                        }).ToList(),
                        Conditions = hotel.Conditionses.Select(y => new ConditionDto
                        {
                            Id = y.Id,
                            Name = y.Name,
                            Images = y.Images.Select(x => x.Url).ToList()
                        }).ToList()
                    };
                });
        }

        public bool AddHotel(HotelDto hotel)
        {
            return this.InvokeInUnitOfWorkScope(
                uow =>
                {
                    var repo = uow.GetRepository<HotelRepository>();
                    var addHotel = new Hotel
                    {
                        Name = hotel.Name,
                        Adress = hotel.Adress,
                        Description = hotel.Description,
                        Stars = hotel.Stars
                    };
                    if (hotel.Images != null)
                    {
                        addHotel.Images = hotel.Images.Select(p => new Image { Url = p }).ToList();
                    }
                    if (!repo.Get().Any(p => p.Name.Equals(hotel.Name, StringComparison.CurrentCultureIgnoreCase)))
                    {
                        repo.Add(addHotel);
                        uow.SaveChanges();
                        return true;
                    }
                    return false;
                });
        }

        public bool EditHotel(HotelDto hoteldto)
        {
            return this.InvokeInUnitOfWorkScope(
                uow =>
                {
                    var repo = uow.GetRepository<HotelRepository>();
                    if (repo.Get().Count(p => p.Name.Equals(hoteldto.Name)) > 1)
                    {
                        return false;
                    }
                    var hotel = repo.GetSingle(p => p.Id == hoteldto.Id);
                    hotel.Name = hoteldto.Name;
                    hotel.Adress = hoteldto.Adress;
                    hotel.Stars = hoteldto.Stars;
                    hotel.Description = hoteldto.Description;
                    repo.Modify(hotel);
                    uow.SaveChanges();
                    return true;
                });
        }

        public void AddHotelImage(int id, string url)
        {
            this.InvokeInUnitOfWorkScope(
                uow =>
                {
                    var repo = uow.GetRepository<HotelRepository>();
                    var hotel = repo.GetSingle(p => p.Id == id);
                    hotel.Images.Add(new Image
                    {
                        Url = url
                    });
                    repo.Modify(hotel);
                    uow.SaveChanges();
                });
        }

        public List<RoomDto> GetHotelRooms(int id)
        {
            return this.InvokeInUnitOfWorkScope(
               uow =>
               {
                   var repo = uow.GetRepository<HotelRepository>();
                   var hotel = repo.GetSingle(p => p.Id == id);
                   return
                       hotel.Rooms.Select(
                           p =>
                           new RoomDto
                           {
                               Images = p.Images.Select(x => x.Url).ToList(),
                               Id = p.Id,
                               Description = p.Description,
                               Number = p.Number,
                               PersonCount = p.PersonCount,
                               Price = p.Price,
                               HotelName = p.Hotel.Name
                           }).ToList();
               });
        }

        public List<HotelDto> GetFreeHotels(DateTime? from, DateTime? to)
        {
            return this.InvokeInUnitOfWorkScope(
                uow =>
                {
                    var repo = uow.GetRepository<OrderRepository>();
                    var hotelRepo = uow.GetRepository<HotelRepository>();
                    var res =
                        repo.Get()
                            .Where(p =>
                                    (p.ArrivalDate < to.Value && p.ArrivalDate > from.Value) ||
                                    (p.LeaveDate < to.Value && p.LeaveDate > from.Value))
                                    .ToList()
                                    .Select(p => p.IdHotel)
                                    .ToList();
                    var result = hotelRepo.Get().ToList();
                    foreach (var id in res)
                    {
                        result.Remove(hotelRepo.GetSingle(p => p.Id == id));
                    }
                    return result.Select(hotel => new HotelDto
                    {
                        Adress = hotel.Adress,
                        Id = hotel.Id,
                        Description = hotel.Description,
                        Name = hotel.Name,
                        Stars = hotel.Stars,
                        Images = hotel.Images.Select(p => p.Url).ToList(),
                        Rooms = hotel.Rooms.Select(z => new RoomDto
                        {
                            Id = z.Id,
                            Description = z.Description,
                            Number = z.Number,
                            PersonCount = z.PersonCount,
                            Images = z.Images.Select(x => x.Url).ToList(),
                            Price = z.Price,
                            HotelName = z.Hotel.Name
                        }).ToList(),
                        Conditions = hotel.Conditionses.Select(y => new ConditionDto
                        {
                            Id = y.Id,
                            Name = y.Name,
                            Images = y.Images.Select(x => x.Url).ToList()
                        }).ToList()
                    }).ToList();
                });
        }

        public void RemoveImage(int id, string imageUrl)
        {
            this.InvokeInUnitOfWorkScope(
              uow =>
              {
                  var repo = uow.GetRepository<HotelRepository>();
                  var hotel = repo.GetSingle(p => p.Id == id);
                  var image = hotel.Images.FirstOrDefault(p => p.Url.Equals(imageUrl));
                  hotel.Images.Remove(image);
                  repo.Modify(hotel);
                  uow.SaveChanges();
              });
        }

        public void AddCondition(int id, string conditionName, string imageUrl)
        {
            this.InvokeInUnitOfWorkScope(
              uow =>
              {
                  var repo = uow.GetRepository<HotelRepository>();
                  var hotel = repo.GetSingle(p => p.Id == id);
                  hotel.Conditionses.Add(new Condition
                  {
                      Name = conditionName,
                      Images = new List<Image> { new Image
                      {
                          Url = imageUrl
                      } }
                  });
                  repo.Modify(hotel);
                  uow.SaveChanges();
              });
        }

        public bool AddRoom(RoomDto room, int id)
        {
            return this.InvokeInUnitOfWorkScope(
              uow =>
              {
                  var repo = uow.GetRepository<HotelRepository>();
                  var hotel = repo.GetSingle(p => p.Id == id);
                  if (hotel.Rooms.Any(p => p.Number == room.Number))
                  {
                      return false;
                  }
                  hotel.Rooms.Add(new Room
                  {
                      Number = room.Number,
                      Description = room.Description,
                      PersonCount = room.PersonCount,
                      Price = room.Price
                  });
                  repo.Modify(hotel);
                  uow.SaveChanges();
                  return true;
              });
        }


        public List<ConditionDto> GetHotelConditions(int id)
        {
            return this.InvokeInUnitOfWorkScope(
               uow =>
               {
                   var repo = uow.GetRepository<HotelRepository>();
                   var hotel = repo.GetSingle(p => p.Id == id);
                   return
                       hotel.Conditionses.Select(
                           p =>
                           new ConditionDto
                           {
                               Images = p.Images.Select(x => x.Url).ToList(),
                               Id = p.Id,
                               Name = p.Name
                           }).ToList();
               });
        }

    }
}
