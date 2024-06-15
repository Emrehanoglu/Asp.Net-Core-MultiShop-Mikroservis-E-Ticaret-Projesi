using MultiShop.Cargo.DataAccessLayer.Abstract;
using MultiShop.Cargo.DataAccessLayer.Concrete;
using MultiShop.Cargo.DataAccessLayer.Repositories;
using MultiShop.Cargo.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Cargo.DataAccessLayer.EntityFramework;

public class EfCargoCustomerDal:GenericRepository<CargoCustomer>,ICargoCustomerDal
{
    private readonly CargoContext _context;
    public EfCargoCustomerDal(CargoContext cargoContext, CargoContext context) : base(cargoContext)
    {
        _context = context;
    }

    public CargoCustomer GetCargoCustomerById(string id)
    {
        var values = _context.CargoCustomers.Where(x => x.UserCustomerId == id).FirstOrDefault();
        return values;
    }
}
