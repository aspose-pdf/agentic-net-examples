using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "feedback_form_v4.pdf";
        const string outputPath = "summary_limited.pdf";
        const string fieldName = "Summary";
        const int charLimit = 30;

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF into FormEditor
            FormEditor formEditor = new FormEditor();
            formEditor.BindPdf(inputPath);

            // Apply a character limit to the "Summary" field
            formEditor.SetFieldLimit(fieldName, charLimit);

            // Save the updated PDF
            formEditor.Save(outputPath);

            Console.WriteLine($"Field limit applied and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}