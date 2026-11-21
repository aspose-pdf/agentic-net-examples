using System;
using System.IO;
using System.Collections.Generic;
using System.Data;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string xmlData = "data.xml";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"PDF file not found: {inputPdf}");
            return;
        }
        if (!File.Exists(xmlData))
        {
            Console.Error.WriteLine($"XML data file not found: {xmlData}");
            return;
        }

        // Define required form field names (as they appear in the PDF)
        var requiredFields = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "FirstName",
            "LastName",
            "Email"
        };

        // ------------------------------------------------------------
        // 1. Load XML data into a DataTable.
        //    Aspose.Pdf.Facades.AutoFiller works with DataTable, not XML.
        // ------------------------------------------------------------
        DataSet ds = new DataSet();
        ds.ReadXml(xmlData);
        DataTable dt = ds.Tables.Count > 0 ? ds.Tables[0] : new DataTable();

        // ------------------------------------------------------------
        // 2. Import the DataTable into the PDF using AutoFiller.
        //    This replaces the missing PdfFormEditor class.
        // ------------------------------------------------------------
        using (AutoFiller filler = new AutoFiller())
        {
            filler.BindPdf(inputPdf);
            filler.ImportDataTable(dt);
            filler.Save(outputPdf);
        }

        // ------------------------------------------------------------
        // 3. Validate that required fields are not empty after import.
        //    Iterate over Document.Form.Fields (Aspose.Pdf.Forms.Field).
        // ------------------------------------------------------------
        using (Document pdfDoc = new Document(outputPdf))
        {
            foreach (var field in pdfDoc.Form.Fields)
            {
                string fieldName = field.PartialName;
                if (requiredFields.Contains(fieldName))
                {
                    string fieldValue = field.Value?.ToString() ?? string.Empty;
                    if (string.IsNullOrWhiteSpace(fieldValue))
                    {
                        Console.WriteLine($"Validation error: Required field '{fieldName}' is empty.");
                    }
                }
            }

            // No further modifications, but saving ensures the document is flushed correctly.
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"Processing completed. Result saved to '{outputPdf}'.");
    }
}
