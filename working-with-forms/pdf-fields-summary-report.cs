using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Folder containing the PDFs to process
        const string inputFolder = "InputPdfs";
        // Path for the generated summary PDF
        const string outputReport = "FieldsSummaryReport.pdf";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Collect all PDF files in the folder
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly);
        if (pdfFiles.Length == 0)
        {
            Console.Error.WriteLine("No PDF files found to process.");
            return;
        }

        // StringBuilder to accumulate the report content
        StringBuilder reportBuilder = new StringBuilder();
        reportBuilder.AppendLine("PDF Fields Summary Report");
        reportBuilder.AppendLine($"Generated on: {DateTime.Now}");
        reportBuilder.AppendLine(new string('=', 40));
        reportBuilder.AppendLine();

        // Process each PDF file
        foreach (string pdfPath in pdfFiles)
        {
            try
            {
                using (Document srcDoc = new Document(pdfPath))
                {
                    // Ensure the document has a form
                    if (srcDoc.Form == null || srcDoc.Form.Count == 0)
                    {
                        reportBuilder.AppendLine($"[ {Path.GetFileName(pdfPath)} ] No form fields found.");
                        reportBuilder.AppendLine();
                        continue;
                    }

                    reportBuilder.AppendLine($"[ {Path.GetFileName(pdfPath)} ]");
                    // Iterate over all fields in the form
                    foreach (Field field in srcDoc.Form.Fields)
                    {
                        // Field name
                        string fieldName = field?.PartialName ?? "(unnamed)";
                        // Field value (may be null)
                        string fieldValue = field?.Value?.ToString() ?? "(empty)";
                        reportBuilder.AppendLine($"  {fieldName}: {fieldValue}");
                    }
                    reportBuilder.AppendLine();
                }
            }
            catch (Exception ex)
            {
                // Log any errors but continue processing other files
                reportBuilder.AppendLine($"[ {Path.GetFileName(pdfPath)} ] Error: {ex.Message}");
                reportBuilder.AppendLine();
            }
        }

        // Create a new PDF document to hold the summary report
        using (Document reportDoc = new Document())
        {
            // Add a single page
            Page reportPage = reportDoc.Pages.Add();

            // Create a TextFragment with the accumulated report text
            TextFragment tf = new TextFragment(reportBuilder.ToString());
            // Set appearance using TextState (DefaultAppearance property does not exist)
            tf.TextState.Font = FontRepository.FindFont("Helvetica");
            tf.TextState.FontSize = 12;
            tf.TextState.ForegroundColor = Color.Black;

            // Add the text fragment to the page
            reportPage.Paragraphs.Add(tf);

            // Save the summary PDF
            reportDoc.Save(outputReport);
        }

        Console.WriteLine($"Summary report saved to '{outputReport}'.");
    }
}
