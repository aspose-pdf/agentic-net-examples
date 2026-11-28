using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "FormD.pdf";
        const string outputPath = "SurveyQCurrent.txt";
        const string fieldName = "SurveyQ";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {pdfPath}");
            return;
        }

        try
        {
            // Form implements IDisposable, so wrap it in a using block.
            using (Form form = new Form())
            {
                // Bind the existing PDF document that contains the form.
                form.BindPdf(pdfPath);

                // Retrieve the current value/option of the field named SurveyQ.
                // GetField returns the field's value as a string.
                string currentOption = form.GetField(fieldName);

                // Write the retrieved value to a text file.
                File.WriteAllText(outputPath, currentOption ?? string.Empty);
            }

            Console.WriteLine($"Current option for '{fieldName}' saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}