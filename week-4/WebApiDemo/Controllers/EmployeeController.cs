using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiDemo.Models;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin,POC")]
public class EmployeeController : ControllerBase
{
    private List<Employee> _employees = new();

    public EmployeeController()
    {
        _employees = GetStandardEmployeeList();
    }

    private List<Employee> GetStandardEmployeeList()
    {
        return new List<Employee>
        {
            new Employee
            {
                Id = 1,
                Name = "John",
                Salary = 5000,
                Permanent = true,
                DateOfBirth = new DateTime(1990, 5, 23),
                Department = new Department { Id = 1, Name = "HR" },
                Skills = new List<Skill> { new Skill { Id = 1, Name = "C#" } }
            }
        };
    }

    [HttpGet]
    [ProducesResponseType(200)]
    public ActionResult<List<Employee>> Get()
    {
       
        return Ok(_employees);
    }

    [HttpPut]
    public ActionResult<Employee> Put([FromBody] Employee emp)
    {
        if (emp.Id <= 0) return BadRequest("Invalid employee id");

        var existing = _employees.FirstOrDefault(e => e.Id == emp.Id);
        if (existing == null) return BadRequest("Invalid employee id");

        existing.Name = emp.Name;
        return Ok(existing);
    }
}
