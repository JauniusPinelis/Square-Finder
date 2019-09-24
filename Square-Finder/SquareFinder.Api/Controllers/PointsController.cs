﻿using System;
using System.Linq;
using System.Net;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using SquareFinder.Api.Repositories;
using SquareFinder.Models;

namespace SquareFinder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PointsController : Controller
    {
        private IDbRepository _repository;
        public PointsController(IDbRepository repository)
        {
            _repository = repository;
        }


        [HttpGet]
        public IActionResult GetPoints(int pointListId)
        {
            //TODO this is not restful need to fix this
            var data = _repository.GetStateInfo();
            return Ok(data);
        }

        [HttpPost]
        public IActionResult AddPoint([FromBody] PointDto pointData)
        {
            var point = new PointEntity(pointData.X, pointData.Y);
            _repository.
            if (point.IsValid(unitOfWork.PointsRepository.GetDb(), pointData.PointListId, ref errorBuilder))
            {
                var pointList = _repository.GetPointListById(pointData.PointListId);
                pointList?.Points.Add(point);
                return CreatedAtRoute("DefaultApi", new { id = point.Id }, point);
            }

            _repository.AddPoint(pointData.PointListId, point);

            return CreatedAtRoute("DefaultApi", new { id = point.Id }, point);  
        }

        [HttpDelete]
        public IActionResult DeletePoint(int pointId)
        {

            PointEntity point = _repository.GetPointById(pointId);
            if (point == null)
            {
                return NotFound();
            }

            _repository.DeletePoint(point);

            return Ok(point);
        }
    }
}