using System.ComponentModel.DataAnnotations;
using System.Net.Mail;





public interface IEmailSender
{
    void Send(string to, string subject);
}

public class SmtpEmailSender : IEmailSender
{
    private readonly SmtpClient _smtpClient;

    public SmtpEmailSender(SmtpClient smtpClient)
    {
        _smtpClient = smtpClient;
    }

    public void Send(string to, string subject)
    {
        var message = new MailMessage("support@domain.com", to)
        {
            Subject = subject
        };
        _smtpClient.Send(message);
    }
}

public interface IEmailValidator
{
    bool IsValid(string email);
}

public class SimpleEmailValidator : IEmailValidator
{
    public bool IsValid(string email) => email.Contains("@");
}

public class User
{
    public string Email { get; set; }
    public string Password { get; set; }

    public User(string email, string password)
    {
        Email = email;
        Password = password;
    }
}

public class UserService
{
    private readonly IEmailSender _emailSender;
    private readonly IEmailValidator _emailValidator;

    public UserService(IEmailSender emailSender, IEmailValidator emailValidator)
    {
        _emailSender = emailSender;
        _emailValidator = emailValidator;
    }

    public void Register(string email, string password)
    {
        if (!_emailValidator.IsValid(email))
            throw new ValidationException("Invalid email address");

        var user = new User(email, password);
        _emailSender.Send(email, "Welcome to our platform!");
    }
}
/*-------------------------------------------------------------------------------------*/
public interface IShape
{
    double GetArea();
}

public interface I3DShape
{
    double GetVolume();
}

public class Circle : IShape
{
    public double Radius { get; set; }
    public double GetArea() => Math.PI * Radius * Radius;
}

public class Rectangle : IShape
{
    public double Width { get; set; }
    public double Height { get; set; }
    public double GetArea() => Width * Height;
}

public class Triangle : IShape
{
    public double Base { get; set; }
    public double Height { get; set; }
    public double GetArea() => 0.5 * Base * Height;
}

public class Cube : I3DShape
{
    public double Side { get; set; }
    public double GetVolume() => Math.Pow(Side, 3);
}

public class Square : IShape, I3DShape
{
    public double Side { get; set; }
    public double GetArea() => Side * Side;
    public double GetVolume() => Math.Pow(Side, 3);
}

public class AreaCalculator
{
    public double TotalArea(IEnumerable<IShape> shapes)
    {
        return shapes.Sum(shape => shape.GetArea());
    }
}

public class VolumeCalculator
{
    public double TotalVolume(IEnumerable<I3DShape> shapes)
    {
        return shapes.Sum(shape => shape.GetVolume());
    }
}

public abstract class Shape2D
{
    public abstract double GetArea();
}

public class Rectangle2D : Shape2D
{
    public double Width { get; set; }
    public double Height { get; set; }
    public override double GetArea() => Width * Height;
}

public class Square2D : Shape2D
{
    public double Side { get; set; }
    public override double GetArea() => Side * Side;
}
