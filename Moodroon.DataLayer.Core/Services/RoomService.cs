using System;
using DataAccessLayer.Models.Models;

namespace Moodroon.DataLayer.Core.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using DataAccessLayer.Repositories.Repositories.DataBaseRepositories;

    using Microsoft.Practices.Unity;

    using Moodroon.DataLayer.Core.DTO;

    public class RoomService : BaseApplicationService
    {
        public RoomService(IUnityContainer container)
            : base(container)
        {
        }

        public RoomDto GetRoom(int id)
        {
            return this.InvokeInUnitOfWorkScope(
               uow =>
               {
                   var repo = uow.GetRepository<RoomRepository>();
                   var room = repo.GetSingle(p => p.Id == id);
                   return new RoomDto
                   {
                       Id = room.Id,
                       Description = room.Description,
                       Images = room.Images.Select(p => p.Url).ToList(),
                       Number = room.Number,
                       PersonCount = room.PersonCount,
                       HotelName = room.Hotel.Name
                   };
               });
        }

        public List<RoomDto> GetAllRooms()
        {
            return this.InvokeInUnitOfWorkScope(
               uow =>
               {
                   var repo = uow.GetRepository<RoomRepository>();
                   var repoHotel = uow.GetRepository<HotelRepository>();
                   var room = repo.Get().ToList();
                   return room.Select(p => new RoomDto
                   {
                       Id = p.Id,
                       Price = p.Price,
                       Description = p.Description,
                       Images = p.Images.Select(x => x.Url).ToList(),
                       Number = p.Number,
                       PersonCount = p.PersonCount,
                       HotelName = repoHotel.GetSingle(y => y.Id == p.IdHotel).Name
                   }).ToList();
               });
        }

        public void DeleteRoom(int id)
        {
            this.InvokeInUnitOfWorkScope(
              uow =>
              {
                  var repo = uow.GetRepository<RoomRepository>();
                  var room = repo.GetSingle(p => p.Id == id);
                  repo.Remove(room);
                  uow.SaveChanges();
              });
        }

        public void UploadImage(int id, string url)
        {
            this.InvokeInUnitOfWorkScope(
              uow =>
              {
                  var repo = uow.GetRepository<RoomRepository>();
                  var room = repo.GetSingle(p => p.Id == id);
                  room.Images.Add(new Image
                  {
                      Url = url
                  });
                  repo.Modify(room);
                  uow.SaveChanges();
              });
        }

        public bool EditRoom(RoomDto roomDto)
        {
            return this.InvokeInUnitOfWorkScope(
              uow =>
              {
                  var repo = uow.GetRepository<RoomRepository>();
                  var repoHotel = uow.GetRepository<HotelRepository>();
                  var room = repo.GetSingle(p => p.Id == roomDto.Id);
                  var hotel = repoHotel.GetSingle(p => p.Name.Equals(roomDto.HotelName, StringComparison.CurrentCultureIgnoreCase));
                  if (hotel.Rooms.Count(p => p.Number == roomDto.Number) > 1)
                  {
                      return false;
                  }
                  room.PersonCount = roomDto.PersonCount;
                  room.Number = roomDto.Number;
                  room.Description = roomDto.Description;
                  room.Price = roomDto.Price;
                  repo.Modify(room);
                  uow.SaveChanges();
                  return true;
              });
        }

        public void RemoveIamge(int id, string imageUrl)
        {
            this.InvokeInUnitOfWorkScope(
              uow =>
              {
                  var repo = uow.GetRepository<RoomRepository>();
                  var room = repo.GetSingle(p => p.Id == id);
                  var image = room.Images.FirstOrDefault(p => p.Url.Equals(imageUrl));
                  room.Images.Remove(image);
                  repo.Modify(room);
                  uow.SaveChanges();
              });
        }

    }
}
