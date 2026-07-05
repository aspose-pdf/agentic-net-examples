using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using Aspose.Pdf.Facades;

class BatchPdfFiller
{
    /// <summary>
    /// Fills a PDF template with multiple data sets and creates separate PDF files.
    /// </summary>
    /// <param name="templatePath">Path to the PDF template containing form fields.</param>
    /// <param name="dataTables">List of DataTable objects, each representing a data set for one output PDF.</param>
    /// <param name="outputDirectory">Directory where the filled PDFs will be saved.</param>
    /// <param name="baseFileName">Base name for generated files (e.g., "filled"). Files will be named "filled0.pdf", "filled1.pdf", ...</param>
    public static void FillTemplateBatch(string templatePath, List<DataTable> dataTables, string outputDirectory, string baseFileName)
    {
        if (!File.Exists(templatePath))
        {
            Console.Error.WriteLine($"Template file not found: {templatePath}");
            return;
        }

        if (!Directory.Exists(outputDirectory))
        {
            Directory.CreateDirectory(outputDirectory);
        }

        for (int i = 0; i < dataTables.Count; i++)
        {
            DataTable dt = dataTables[i];
            string outputPath = Path.Combine(outputDirectory, $"{baseFileName}{i}.pdf");

            // Ensure the output stream is properly disposed.
            using (FileStream outStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            // AutoFiller implements IDisposable, so wrap it in a using block.
            using (AutoFiller filler = new AutoFiller())
            {
                // Bind the template PDF.
                filler.BindPdf(templatePath);

                // Import the current data set.
                filler.ImportDataTable(dt);

                // Direct the filled result to the file stream.
                filler.OutputStream = outStream;

                // Save the filled PDF.
                filler.Save();
            }

            Console.WriteLine($"Generated: {outputPath}");
        }
    }

    // Example usage.
    static void Main()
    {
        string template = "TemplateForm.pdf";
        string outputDir = "FilledOutputs";
        string baseName = "FilledDoc";

        // Prepare sample data tables.
        var tables = new List<DataTable>();

        // First data set.
        DataTable dt1 = new DataTable("DataSet1");
        dt1.Columns.Add("FirstName", typeof(string));
        dt1.Columns.Add("LastName", typeof(string));
        dt1.Columns.Add("Address", typeof(string));
        dt1.Rows.Add("John", "Doe", "123 Main St");
        tables.Add(dt1);

        // Second data set.
        DataTable dt2 = new DataTable("DataSet2");
        dt2.Columns.Add("FirstName", typeof(string));
        dt2.Columns.Add("LastName", typeof(string));
        dt2.Columns.Add("Address", typeof(string));
        dt2.Rows.Add("Jane", "Smith", "456 Oak Ave");
        tables.Add(dt2);

        // Perform batch filling.
        FillTemplateBatch(template, tables, outputDir, baseName);
    }
}