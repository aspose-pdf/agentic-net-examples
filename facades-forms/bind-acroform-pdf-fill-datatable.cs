using System;
using System.Data;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for input AcroForm PDF and output filled PDF
        const string inputPdfPath  = "template_acroform.pdf";
        const string outputPdfPath = "filled_output.pdf";

        // Verify the template exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Template PDF not found: {inputPdfPath}");
            return;
        }

        // Create a synthetic DataTable whose column names match the form field names
        DataTable formData = new DataTable();
        formData.Columns.Add("Name", typeof(string));
        formData.Columns.Add("Date", typeof(string));
        formData.Columns.Add("Amount", typeof(string));

        // Add a single row of sample data
        formData.Rows.Add("John Doe", DateTime.Today.ToShortDateString(), "$123.45");

        // Load the AcroForm PDF inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Initialize the AutoFiller facade
            using (AutoFiller filler = new AutoFiller())
            {
                // Bind the loaded PDF to the filler
                filler.BindPdf(pdfDocument);

                // Import the DataTable – this populates fields whose PartialName matches column names
                filler.ImportDataTable(formData);

                // Save the filled PDF to the specified output path
                filler.Save(outputPdfPath);

                // Close the facade (optional when used inside a using block)
                filler.Close();
            }
        }

        Console.WriteLine($"Form filled and saved to '{outputPdfPath}'.");
    }
}
