using System;
using System.Data;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the template and the final combined PDF
        const string templatePath = "template.pdf";
        const string combinedOutputPath = "combined.pdf";

        if (!File.Exists(templatePath))
        {
            Console.Error.WriteLine($"Template not found: {templatePath}");
            return;
        }

        // -----------------------------------------------------------------
        // 1. Create synthetic records that match the form field names
        // -----------------------------------------------------------------
        var syntheticRecords = new List<Dictionary<string, string>>
        {
            new Dictionary<string, string>
            {
                { "Name",  "Alice" },
                { "Age",   "30"    },
                { "Agree", "true"  }   // Checkbox field expects "true"/"false"
            },
            new Dictionary<string, string>
            {
                { "Name",  "Bob"   },
                { "Age",   "25"    },
                { "Agree", "false" }
            }
        };

        // -----------------------------------------------------------------
        // 2. Build a DataTable whose columns correspond to the PDF fields
        // -----------------------------------------------------------------
        DataTable dataTable = new DataTable();
        dataTable.Columns.Add("Name",  typeof(string));
        dataTable.Columns.Add("Age",   typeof(string));
        dataTable.Columns.Add("Agree", typeof(string));

        foreach (var record in syntheticRecords)
        {
            DataRow row = dataTable.NewRow();
            row["Name"]  = record["Name"];
            row["Age"]   = record["Age"];
            row["Agree"] = record["Agree"];
            dataTable.Rows.Add(row);
        }

        // -----------------------------------------------------------------
        // 3. Fill the template for each record and store the result in temp files
        // -----------------------------------------------------------------
        var tempFiles = new List<string>();
        foreach (DataRow dataRow in dataTable.Rows)
        {
            // Create a one‑row DataTable for the current record
            DataTable singleRowTable = dataTable.Clone();
            singleRowTable.ImportRow(dataRow);

            // Generate a temporary file name
            string tempPath = Path.GetTempFileName();
            tempFiles.Add(tempPath);

            // Use AutoFiller to import the data and save the filled PDF
            using (Aspose.Pdf.Facades.AutoFiller filler = new Aspose.Pdf.Facades.AutoFiller())
            {
                filler.BindPdf(templatePath);
                filler.ImportDataTable(singleRowTable);
                filler.Save(tempPath);
            }
        }

        // -----------------------------------------------------------------
        // 4. Merge all filled PDFs into a single document
        // -----------------------------------------------------------------
        if (tempFiles.Count > 0)
        {
            using (Aspose.Pdf.Document target = new Aspose.Pdf.Document(tempFiles[0]))
            {
                for (int i = 1; i < tempFiles.Count; i++)
                {
                    using (Aspose.Pdf.Document source = new Aspose.Pdf.Document(tempFiles[i]))
                    {
                        target.Pages.Add(source.Pages);
                    }
                }

                target.Save(combinedOutputPath);
            }
        }

        // -----------------------------------------------------------------
        // 5. Clean up temporary files
        // -----------------------------------------------------------------
        foreach (string file in tempFiles)
        {
            try { File.Delete(file); } catch { /* ignore cleanup errors */ }
        }

        Console.WriteLine($"Combined PDF created at: {combinedOutputPath}");
    }
}