using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "feedback_template.pdf";
        const string outputPath = "cleaned_rating.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Create a FormEditor to manipulate form fields
            using (FormEditor editor = new FormEditor())
            {
                // Load the PDF document
                editor.BindPdf(inputPath);

                // Delete the list item "Bad" from the ListBox named "Rating"
                // Correct method name is DelListItem (not DeleteListBoxItem)
                editor.DelListItem("Rating", "Bad");

                // Save the modified PDF
                editor.Save(outputPath);
            }

            Console.WriteLine($"Cleaned PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
