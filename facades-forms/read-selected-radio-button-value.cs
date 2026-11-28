using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "RadioForm.pdf";
        const string txtPath = "SelectedGender.txt";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {pdfPath}");
            return;
        }

        // Bind the PDF and work with its form fields using the Form facade
        using (Form form = new Form())
        {
            form.BindPdf(pdfPath);

            // Retrieve the selected value of the radio button field named "GenderButton"
            // The Form facade provides GetField which returns the field's value as a string.
            string selected = form.GetField("GenderButton");

            // Save the selected value to a text file (empty string if null)
            File.WriteAllText(txtPath, selected ?? string.Empty);
            Console.WriteLine($"Selected gender '{selected}' saved to {txtPath}");
        }
    }
}
