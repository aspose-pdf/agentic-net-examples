using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "protected_form.pdf";
        const string correctPassword = "owner123";
        const string wrongPassword = "wrongPass";

        // Attempt to open the PDF with an incorrect password.
        try
        {
            using (Document docWrong = new Document(inputPdf, wrongPassword))
            {
                // If no exception, the password was somehow accepted (unexpected).
                Console.WriteLine("Unexpectedly opened PDF with wrong password.");
            }
        }
        catch (InvalidPasswordException)
        {
            Console.WriteLine("Access denied: incorrect password prevents opening the document.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error while opening with wrong password: {ex.Message}");
        }

        // Open the PDF with the correct password and read a protected field.
        try
        {
            using (Document docCorrect = new Document(inputPdf, correctPassword))
            {
                // Bind the opened document to the Form facade.
                Form form = new Form(docCorrect);

                // Replace "PasswordField" with the actual field name in the PDF.
                string fieldName = "PasswordField";

                // Retrieve the field value.
                string fieldValue = form.GetField(fieldName);
                Console.WriteLine($"Field '{fieldName}' value: {fieldValue}");
            }
        }
        catch (InvalidPasswordException)
        {
            Console.WriteLine("Failed to open PDF with the correct password.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error while accessing the field: {ex.Message}");
        }
    }
}