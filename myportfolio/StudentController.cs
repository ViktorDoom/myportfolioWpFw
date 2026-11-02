using Microsoft.AspNetCore.Mvc;
using myportfolio.student;
namespace myportfolio;


[Route("api/[controller]")]
public class StudentController : ControllerBase
{
    
}

[HttpGet("student")]
public student GetStudent()
{
    student student = new student();
    return student;
}