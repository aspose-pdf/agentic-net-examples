using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "form_summary.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPath))
            {
                // Access the AcroForm (if any)
                Aspose.Pdf.Forms.Form acroForm = doc.Form;
                int totalFields = 0;
                int submitActions = 0;

                if (acroForm != null && acroForm.Fields != null)
                {
                    // Total number of form fields
                    totalFields = acroForm.Fields.Length;

                    // Count how many of those fields are button fields (commonly used for submit actions)
                    foreach (Aspose.Pdf.Forms.Field field in acroForm.Fields)
                    {
                        if (field is ButtonField)
                        {
                            submitActions++;
                        }
                    }
                }

                // Prepare summary text
                string summary = $"Total form fields: {totalFields}{Environment.NewLine}Submit actions: {submitActions}{Environment.NewLine}";

                // Write summary to a text file
                File.WriteAllText(outputPath, summary);
                Console.WriteLine($"Summary written to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
