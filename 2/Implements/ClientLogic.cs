using DatabaseImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessLogic.BindingModel;
using BusinessLogic.Interfaces;
using BusinessLogic.ViewModel;

namespace DatabaseImplement.Implements
{
    public class ClientLogic : IClientLogic
    {
        public void CreateOrUpdate(ClientBindingModel model)
        {
            using (var context = new KursachDatabase())
            {
                Client element = model.Id.HasValue ? null : new Client();                
                if (model.Id.HasValue)
                {
                    element = context.Clients.FirstOrDefault(rec => rec.Id == model.Id);
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                }
                else
                {
                    element = new Client();
                    context.Clients.Add(element);
                }
                element.Email = model.Email;
                element.PhoneNumber = model.PhoneNumber;
                element.UserId = model.UserId;
                element.Pasport = model.Pasport;
                element.ClientFIO = model.ClientFIO;
                context.SaveChanges();
            }
        }
        public void Delete(ClientBindingModel model)
        {
            using (var context = new KursachDatabase())
            {
                Client element = context.Clients.FirstOrDefault(rec => rec.UserId == model.Id);

                if (element != null)
                {
                    context.Clients.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }
        public List<ClientViewModel> Read(ClientBindingModel model)
        {
            using (var context = new KursachDatabase())
            {
                return context.Clients
                 .Where(rec => model == null
                ||(rec.Id==model.Id)
                   ||(model.Pasport==rec.Pasport))
               .Select(rec => new ClientViewModel
                {
                    Id = rec.Id,
                   UserId = rec.UserId,
                    ClientFIO = rec.ClientFIO,
                    Email = rec.Email,
                   Pasport = rec.Pasport,
                    PhoneNumber=rec.PhoneNumber,
                })
                .ToList();
            }
        }


    }
}