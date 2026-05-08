using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all form fields and set MaxLen for password fields
            foreach (Field field in doc.Form)
            {
                if (field is PasswordBoxField pwdField)
                {
                    pwdField.MaxLen = 20; // enforce maximum length of 20 characters
                }
            }

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Updated PDF saved to '{outputPath}'.");
    }
}