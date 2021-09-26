﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using StarChart.Data;
using StarChart.Models;

namespace StarChart.Controllers
{
    [Route("")]
    public class CelestialObjectController : ControllerBase
    {
        public CelestialObjectController(ApplicationDbContext context)
        {
            _context = context;
        }

        public readonly ApplicationDbContext _context;

        [HttpGet("{id:int}", Name ="GetById")]
        public IActionResult GetById(int id)
        {
            var celestialObject = _context.CelestialObjects.FirstOrDefault(x => x.Id == id); 

            if(celestialObject == null)
            {
                return NotFound();
            }

            celestialObject.Satellites = GetSatellites(celestialObject);

            return Ok(celestialObject);
        }

        [HttpGet("{name}")]
        public IActionResult GetByName(string name)
        {
            var celestialObjects = _context.CelestialObjects
                .Where(x => x.Name == name)
                .ToList();

            if (!celestialObjects.Any())
            {
                return NotFound();
            }

            foreach (var celestialObject in celestialObjects)
            {
                celestialObject.Satellites = GetSatellites(celestialObject);
            }

            return Ok(celestialObjects);
        }

        [HttpGet()]
        public IActionResult GetAll()
        {
            var celestialObjects = _context.CelestialObjects.ToList();

            foreach(var celestialObject in celestialObjects)
            {
                celestialObject.Satellites = GetSatellites(celestialObject);
            }

            return Ok(celestialObjects);
        }

        private List<CelestialObject> GetSatellites(CelestialObject celestialObject)
        {
           return _context.CelestialObjects
                            .Where(x => x.OrbitedObjectId == celestialObject.Id)
                            .ToList();
        }
    }
}
