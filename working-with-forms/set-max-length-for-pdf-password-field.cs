using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // source PDF containing the password field
        const string outputPdf = "output.pdf";  // PDF after setting the max length
        const string passwordFieldName = "Password"; // exact name of the password field

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Retrieve the field by its full name and cast to PasswordBoxField
            PasswordBoxField pwdField = doc.Form[passwordFieldName] as PasswordBoxField;

            if (pwdField != null)
            {
                // Enforce a maximum of 20 characters for the password field
                pwdField.MaxLen = 20;
                Console.WriteLine($"Set MaxLen=20 for field '{passwordFieldName}'.");
            }
            else
            {
                Console.Error.WriteLine($"Password field '{passwordFieldName}' not found or is not a PasswordBoxField.");
            }

            // Save the modified document
            doc.Save(outputPdf);
            Console.WriteLine($"Modified PDF saved to '{outputPdf}'.");
        }
    }
}