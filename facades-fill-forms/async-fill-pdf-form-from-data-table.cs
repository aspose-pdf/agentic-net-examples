using System;
using System.Data;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    // Entry point – async Main is supported in C# 7.1+
    static async Task Main(string[] args)
    {
        // Paths – adjust as needed
        const string xlsxPath = "data.xlsx";          // not used – kept for signature compatibility
        const string pdfTemplatePath = "template.pdf"; // will be created on‑the‑fly if missing
        const string outputPdfPath = "filled_output.pdf";

        // Ensure a PDF template with the required form fields exists.
        // This makes the example self‑contained and avoids FileNotFoundException.
        CreatePdfTemplateIfMissing(pdfTemplatePath);

        // Perform the fill operation asynchronously.
        await FillPdfFromXlsxAsync(xlsxPath, pdfTemplatePath, outputPdfPath);
        Console.WriteLine($"PDF saved to '{outputPdfPath}'.");
    }

    /// <summary>
    /// Creates a minimal PDF template containing three text box fields (FirstName, LastName, Address).
    /// The method is called only when the template file does not already exist.
    /// </summary>
    private static void CreatePdfTemplateIfMissing(string templatePath)
    {
        if (File.Exists(templatePath))
            return;

        // Create a new PDF document.
        using var doc = new Document();
        var page = doc.Pages.Add();

        // Helper to add a TextBoxField at a given rectangle.
        void AddField(string partialName, float llx, float lly, float urx, float ury)
        {
            var field = new TextBoxField(page, new Rectangle(llx, lly, urx, ury))
            {
                PartialName = partialName,
                Value = string.Empty
            };
            doc.Form.Add(field);
        }

        // Add three fields that match the column names used later.
        AddField("FirstName", 100, 700, 300, 720);
        AddField("LastName",  100, 660, 300, 680);
        AddField("Address",   100, 620, 400, 640);

        // Save the template.
        doc.Save(templatePath);
    }

    /// <summary>
    /// Reads an XLSX file and a PDF template asynchronously, fills the PDF using
    /// Aspose.Pdf.Facades.AutoFiller, and writes the resulting PDF asynchronously.
    /// In this self‑contained example the XLSX file is not actually read – a DataTable
    /// is built in‑memory – but the method still demonstrates async file I/O for the
    /// template and the output PDF.
    /// </summary>
    private static async Task FillPdfFromXlsxAsync(
        string xlsxFilePath,
        string pdfTemplateFilePath,
        string outputPdfFilePath,
        CancellationToken cancellationToken = default)
    {
        // ------------------------------------------------------------
        // 1. Build a DataTable that represents the data to fill.
        //    In a real scenario you would parse the XLSX into this table.
        // ------------------------------------------------------------
        var dataTable = new DataTable("FormData");
        dataTable.Columns.Add("FirstName", typeof(string));
        dataTable.Columns.Add("LastName",  typeof(string));
        dataTable.Columns.Add("Address",   typeof(string));

        var row = dataTable.NewRow();
        row["FirstName"] = "John";
        row["LastName"]  = "Doe";
        row["Address"]   = "123 Main St, Anytown";
        dataTable.Rows.Add(row);

        // ------------------------------------------------------------
        // 2. Asynchronously read the PDF template into a memory stream.
        // ------------------------------------------------------------
        await using (var pdfTemplateStream = new FileStream(
            pdfTemplateFilePath,
            FileMode.Open,
            FileAccess.Read,
            FileShare.Read,
            bufferSize: 8192,
            useAsync: true))
        {
            var pdfTemplateMemory = new MemoryStream();
            await pdfTemplateStream.CopyToAsync(pdfTemplateMemory, cancellationToken);
            pdfTemplateMemory.Position = 0; // Reset for binding.

            // ------------------------------------------------------------
            // 3. Use AutoFiller to bind the template, import the DataTable,
            //    and generate the filled PDF into a memory stream.
            // ------------------------------------------------------------
            using var autoFiller = new AutoFiller();
            autoFiller.BindPdf(pdfTemplateMemory);
            autoFiller.ImportDataTable(dataTable);

            using var filledPdfMemory = new MemoryStream();
            autoFiller.Save(filledPdfMemory);
            filledPdfMemory.Position = 0; // Prepare for async write.

            // ------------------------------------------------------------
            // 4. Asynchronously write the filled PDF to the output file.
            // ------------------------------------------------------------
            await using var outputStream = new FileStream(
                outputPdfFilePath,
                FileMode.Create,
                FileAccess.Write,
                FileShare.None,
                bufferSize: 8192,
                useAsync: true);

            await filledPdfMemory.CopyToAsync(outputStream, cancellationToken);
        }
    }
}
