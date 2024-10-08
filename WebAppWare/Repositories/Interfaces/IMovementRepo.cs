﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebAppWare.Database.Entities;
using WebAppWare.Models;

namespace WebAppWare.Repositories.Interfaces;

public interface IMovementRepo
{
	Task<List<WarehouseMovementModel>> GetAll();
	Task<WarehouseMovementModel> GetById(int id);
	Task Create(WarehouseMovementModel model);
	Task<WarehouseMovementModel> GetLastMovement();
	Task DeleteById(int id);
	Task<string> SetMovementNumber(DateTime date, MovementType moveType);
	Task<bool> IsQtyEnoughToCreateWz(WarehouseMovementModel model);
	List<ProductFlowModel> GetProductFlowsFromForm(ProductFlowMovementModel model);
	Task<bool> IsPossibleToDeletePzWz(int id);
}
