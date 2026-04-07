using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class FormDataComparer
{
    static void Main()
    {
        const string pdfPath1 = "form1.pdf";
        const string pdfPath2 = "form2.pdf";
        const string reportPath = "form_diff_report.pdf";

        if (!File.Exists(pdfPath1) || !File.Exists(pdfPath2))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Load the first PDF form
        using (Form form1 = new Form())
        {
            form1.BindPdf(pdfPath1);

            // Load the second PDF form
            using (Form form2 = new Form())
            {
                form2.BindPdf(pdfPath2);

                // Collect all unique field names from both documents
                var fieldsSet = new System.Collections.Generic.HashSet<string>(StringComparer.Ordinal);
                foreach (string name in form1.FieldNames) fieldsSet.Add(name);
                foreach (string name in form2.FieldNames) fieldsSet.Add(name);

                // Build a diff report
                var reportBuilder = new StringBuilder();
                reportBuilder.AppendLine("Form Data Comparison Report");
                reportBuilder.AppendLine($"File 1: {Path.GetFileName(pdfPath1)}");
                reportBuilder.AppendLine($"File 2: {Path.GetFileName(pdfPath2)}");
                reportBuilder.AppendLine(new string('=', 40));
                reportBuilder.AppendLine();

                bool anyDifferences = false;

                foreach (string fieldName in fieldsSet)
                {
                    object val1Obj = form1.GetField(fieldName);
                    object val2Obj = form2.GetField(fieldName);
                    string val1 = val1Obj?.ToString() ?? "<null>";
                    string val2 = val2Obj?.ToString() ?? "<null>";

                    if (!string.Equals(val1, val2, StringComparison.Ordinal))
                    {
                        anyDifferences = true;
                        reportBuilder.AppendLine($"Field: {fieldName}");
                        reportBuilder.AppendLine($"  PDF 1 Value: {val1}");
                        reportBuilder.AppendLine($"  PDF 2 Value: {val2}");
                        reportBuilder.AppendLine();
                    }
                }

                if (!anyDifferences)
                {
                    reportBuilder.AppendLine("No differences were found. All form fields match.");
                }

                // Create a PDF document to hold the report
                using (Document reportDoc = new Document())
                {
                    // Add a blank page
                    Aspose.Pdf.Page page = reportDoc.Pages.Add();

                    // Create a TextFragment with the report text
                    TextFragment fragment = new TextFragment(reportBuilder.ToString())
                    {
                        // Optional styling
                        TextState = { FontSize = 12, ForegroundColor = Aspose.Pdf.Color.Black }
                    };

                    // Add the fragment to the page
                    page.Paragraphs.Add(fragment);

                    // Save the report PDF
                    reportDoc.Save(reportPath);
                }

                Console.WriteLine($"Form comparison report saved to '{reportPath}'.");
            }
        }
    }
}