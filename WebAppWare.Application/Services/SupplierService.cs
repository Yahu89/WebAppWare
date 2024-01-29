using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppWare.Domain.Interfaces;
using WebAppWare.Infrastructure;

namespace WebAppWare.Application.Services;

public class SupplierService : ISupplierService
{
    private readonly ISupplierRepo _supplierRepo;
    public SupplierService(ISupplierRepo supplierRepo)
    {
        _supplierRepo = supplierRepo;
    }

	public async Task Create(Supplier supplier)
	{
		await _supplierRepo.Create(supplier);
	}

	public async Task Delete(Supplier supplier)
	{
		await _supplierRepo.Delete(supplier);
	}

	public async Task<List<Supplier>> GetAll()
    {
        return await _supplierRepo.GetAll();
    }

	public async Task<Supplier> GetById(int id)
	{
		return await _supplierRepo.GetById(id);
	}

	public async Task Update(Supplier supplier)
	{
		await _supplierRepo.Update(supplier);
	}
}
