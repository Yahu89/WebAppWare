using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppWare.Infrastructure;

namespace WebAppWare.Application.Services;

public interface IMovementService
{
	Task<List<WarehouseMovement>> GetAll();
}
