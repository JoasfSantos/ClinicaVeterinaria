using System.Text.RegularExpressions;


namespace ClinicaVet.Utilidades;
public class ValidadorEmail // Regex E-mail.
{
    private readonly string _regexEmail = @"^[a-zA-Z0-9_-]+@[a-z]+\.com(\.br)?$";
    private readonly Regex _regex;

    public ValidadorEmail()
    {
        _regex = new Regex(_regexEmail);
    }

    public bool ValidarEmail(string email)
    {
        Match match = _regex.Match(email);

        if (match.Success)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}