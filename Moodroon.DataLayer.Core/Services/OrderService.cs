using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using DataAccessLayer.Models.Models;
using DataAccessLayer.Repositories.Repositories.DataBaseRepositories;
using Microsoft.Practices.Unity;
using Moodroon.DataLayer.Core.DTO;

namespace Moodroon.DataLayer.Core.Services
{
    public class OrderService : BaseApplicationService
    {
        public OrderService(IUnityContainer container) : base(container)
        {
        }
        public List<UiOrderDto> GetUserOrdersDtos(int id)
        {
            return this.InvokeInUnitOfWorkScope(uow =>
            {
                var orders = uow.GetRepository<UserRepository>().GetSingle(p => p.Id == id).Orders.ToList();
                return orders.Select(p => new UiOrderDto
                {
                    CountOfDays = p.CountOfDays,
                    Id = p.Id,
                    HotelName = uow.GetRepository<HotelRepository>().GetSingle(x => x.Id == p.IdHotel).Name,
                    UserName = uow.GetRepository<UserRepository>().GetSingle(x => x.Id == p.IdUser).UserName,
                    RoomNumber = uow.GetRepository<RoomRepository>().GetSingle(x => x.Id == p.IdRoom).Number,
                    TotalPrice = p.TotalPrice,
                    ArrivalDate = p.ArrivalDate.ToString(CultureInfo.CurrentCulture),
                    LeaveDate = p.LeaveDate.ToString(CultureInfo.CurrentCulture)
                }).ToList();
            });
        }

        public void DeleteOrder(int id,int deleteId)    
        {
            this.InvokeInUnitOfWorkScope(uow =>
            {
                var repo = uow.GetRepository<OrderRepository>();
                var user = repo.GetSingle(p => p.Id == deleteId);
                repo.Remove(user);
                uow.SaveChanges();
            });
        }


        public List<UiOrderDto> GetAllOrders()
        {
            return this.InvokeInUnitOfWorkScope(uow =>
            {
                var repo = uow.GetRepository<OrderRepository>();
                return repo.Get().ToList().Select(p => new UiOrderDto
                {
                    CountOfDays = p.CountOfDays,
                    Id = p.Id,
                    HotelName = uow.GetRepository<HotelRepository>().GetSingle(x => x.Id == p.IdHotel).Name,
                    UserName = uow.GetRepository<UserRepository>().GetSingle(x => x.Id == p.IdUser).UserName,
                    RoomNumber = uow.GetRepository<RoomRepository>().GetSingle(x => x.Id == p.IdRoom).Number,
                    TotalPrice = p.TotalPrice,
                    ArrivalDate = p.ArrivalDate.ToString(CultureInfo.CurrentCulture),
                    LeaveDate = p.LeaveDate.ToString(CultureInfo.CurrentCulture)
                }).ToList();
            });
        }

        public void DeleteOrder(int id)
        {
            this.InvokeInUnitOfWorkScope(uow =>
            {
                var repo = uow.GetRepository<OrderRepository>();
                var order = repo.GetSingle(p => p.Id == id);
                repo.Remove(order);
                uow.SaveChanges();
            });
        }

      
        public bool EditOrder(OrderDto orderDto)
        {
            return this.InvokeInUnitOfWorkScope(uow =>
            {

                var repo = uow.GetRepository<OrderRepository>();
                var editOrder = repo.GetSingle(p => p.Id == orderDto.Id);
                var hotelRepo = uow.GetRepository<HotelRepository>();
                var userRepo = uow.GetRepository<UserRepository>();
                var roomRepo = uow.GetRepository<RoomRepository>();
                var room = roomRepo.Get(p => p.Number == orderDto.RoomNumber).FirstOrDefault();
                var user = userRepo.GetSingle(p => p.UserName.Equals(orderDto.UserName, StringComparison.CurrentCultureIgnoreCase));
                var hotel = hotelRepo.GetSingle(p => p.Name.Equals(orderDto.HotelName, StringComparison.CurrentCultureIgnoreCase));
                if (room != null)
                {
                    var roomOrders =
                        room.Orders.FirstOrDefault(
                            p => (p.ArrivalDate > orderDto.ArrivalDate && p.ArrivalDate < orderDto.LeaveDate) || (
                                p.LeaveDate > orderDto.ArrivalDate && p.LeaveDate < orderDto.LeaveDate));
                    if (roomOrders == null)
                    {
                        editOrder.Room = room;
                        editOrder.CountOfDays = orderDto.CountOfDays;
                        editOrder.Hotel = hotel;
                        editOrder.LeaveDate = orderDto.LeaveDate;
                        editOrder.ArrivalDate = orderDto.ArrivalDate;
                        editOrder.User = user;
                        editOrder.TotalPrice = orderDto.TotalPrice;
                        repo.Modify(editOrder);
                        uow.SaveChanges();
                        return true;
                    }
                }
                return false;
            });
        }

        public bool AddOrder(OrderDto orderDto)
        {
            return this.InvokeInUnitOfWorkScope(uow =>
            {
                var repo = uow.GetRepository<OrderRepository>();
                var hotelRepo = uow.GetRepository<HotelRepository>();
                var userRepo = uow.GetRepository<UserRepository>();
                var roomRepo = uow.GetRepository<RoomRepository>();
                var room = roomRepo.Get(p => p.Number == orderDto.RoomNumber).FirstOrDefault();
                var user =
                    userRepo.GetSingle(
                        p => p.UserName.Equals(orderDto.UserName, StringComparison.CurrentCultureIgnoreCase));
                var hotel =
                    hotelRepo.GetSingle(
                        p => p.Name.Equals(orderDto.HotelName, StringComparison.CurrentCultureIgnoreCase));
                if (room != null)
                {
                    var roomOrders =
                        room.Orders.FirstOrDefault(
                            p => (p.ArrivalDate > orderDto.ArrivalDate && p.ArrivalDate < orderDto.LeaveDate) || (
                                p.LeaveDate > orderDto.ArrivalDate && p.LeaveDate < orderDto.LeaveDate));
                    if (roomOrders == null)
                    {
                        repo.Add(new Order
                        {
                            Room = room,
                            CountOfDays = orderDto.CountOfDays,
                            Hotel = hotel,
                            LeaveDate = orderDto.LeaveDate,
                            ArrivalDate = orderDto.ArrivalDate,
                            User = user,
                            TotalPrice = orderDto.TotalPrice

                        });
                        uow.SaveChanges();
                        return true;
                    }
                }
                return false;
            });
        }
    }
}
