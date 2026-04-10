using System;
using System.Data;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Aspose.Pdf.Facades;

class Program
{
    static async Task Main()
    {
        const string templatePdf = "template.pdf";
        const string excelSource = "data.xlsx";
        const string outputPdf   = "filled.pdf";

        using var cancellation = new CancellationTokenSource();
        CancellationToken token = cancellation.Token;

        // Load the Excel data – if the file does not exist we fall back to an empty DataTable
        DataTable data;
        try
        {
            data = await LoadExcelAsync(excelSource, token);
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine($"Warning: Excel source file '{excelSource}' not found. Continuing with an empty data set.");
            data = new DataTable();
        }

        // Fill the PDF using the AutoFiller facade
        using (AutoFiller filler = new AutoFiller())
        {
            filler.BindPdf(templatePdf);          // bind the PDF template
            filler.ImportDataTable(data);        // import data from the Excel sheet

            // Save the filled PDF into a memory stream first (AutoFiller has no async API)
            using (MemoryStream pdfStream = new MemoryStream())
            {
                filler.Save(pdfStream);
                pdfStream.Position = 0;

                // Asynchronously write the memory stream to the output file
                await using (FileStream fs = new FileStream(
                    outputPdf,
                    FileMode.Create,
                    FileAccess.Write,
                    FileShare.None,
                    bufferSize: 4096,
                    useAsync: true))
                {
                    await pdfStream.CopyToAsync(fs, token);
                }
            }
        }

        Console.WriteLine($"Filled PDF saved to '{outputPdf}'.");
    }

    // Asynchronously loads an Excel file and returns a DataTable.
    // Replace the stub implementation with a real Excel parser as needed.
    private static async Task<DataTable> LoadExcelAsync(string path, CancellationToken token)
    {
        if (!File.Exists(path))
            throw new FileNotFoundException($"Excel source file not found: {path}", path);

        // Read the file into a memory stream using async I/O
        await using (FileStream fs = new FileStream(
            path,
            FileMode.Open,
            FileAccess.Read,
            FileShare.Read,
            bufferSize: 4096,
            useAsync: true))
        await using (MemoryStream ms = new MemoryStream())
        {
            await fs.CopyToAsync(ms, token);
            ms.Position = 0;

            // TODO: Parse the Excel content from the memory stream into a DataTable.
            // For demonstration purposes, return an empty DataTable.
            return new DataTable();
        }
    }
}
