using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebAppWare.Models;

namespace WebAppWare.Repositories.Interfaces;

public interface IMovementRepo
{
	Task<List<MovementModel>> GetAll();
	Task<MovementModel> GetById(int id);
	Task<bool> IsDocumentNameUnique(string inputName);
	Task Create(MovementModel model);
	Task<MovementModel> GetLastMovement();
	Task DeleteById(int id);
}
