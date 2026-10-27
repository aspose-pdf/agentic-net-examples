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
        const string buttonName = "SubmitBtn";
        const string tooltipText = "Please fill in all required fields before submitting.";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            using (Document doc = new Document(inputPath))
            {
                // Locate the submit button by its name
                ButtonField submitButton = null;
                foreach (var field in doc.Form)
                {
                    if (field is ButtonField btn && btn.Name == buttonName)
                    {
                        submitButton = btn;
                        break;
                    }
                }

                if (submitButton == null)
                {
                    Console.Error.WriteLine($"Button field '{buttonName}' not found.");
                }
                else
                {
                    // Assign tooltip (alternate caption) to the button
                    submitButton.AlternateCaption = tooltipText; // Correct property for tooltip
                }

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF saved with tooltip to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
