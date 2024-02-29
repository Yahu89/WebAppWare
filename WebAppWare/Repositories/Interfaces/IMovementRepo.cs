using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebAppWare.Database.Entities;
using WebAppWare.Models;

namespace WebAppWare.Repositories.Interfaces;

public interface IMovementRepo
{
	Task<List<MovementModel>> GetAll();
	Task<MovementModel> GetById(int id);
	Task<bool> IsDocumentNameUnique(string inputName);
	Task<int> Create(MovementModel model);
	Task<MovementModel> GetLastMovement();
	Task DeleteById(int id);
	Task<string> SetMovementNumber(DateTime date, MovementType moveType);
	Task<bool> IsPossibleToCreateWz(ProductFlowMovementModel model, IEnumerable<ProductFlowModel> itemCodes);
	List<ProductFlowModel> GetProductFlowsFromForm(ProductFlowMovementModel model);
	bool IsUniqueAndQtyCorrectForPzWz(List<ProductFlowModel> itemCodes);
	Task<bool> IsPossibleToDeletePzWz(int id);
	MovementModel FromCollectionToMovementModel(IFormCollection collection, MovementType type);
}
