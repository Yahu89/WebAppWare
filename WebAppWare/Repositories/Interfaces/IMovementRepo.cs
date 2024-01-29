using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebAppWare.Models;

namespace WebAppWare.Repositories.Interfaces;

public interface IMovementRepo
{
	Task<List<WarehouseMovementModel>> GetAll();

}
