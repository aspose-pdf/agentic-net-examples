using System;
using System.IO;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Multithreading;

class Program
{
    static void Main()
    {
        const string templatePath = "template.pdf";
        const string outputPath = "filled.pdf";
        const int timeoutSeconds = 5;

        if (!File.Exists(templatePath))
        {
            Console.Error.WriteLine($"File not found: {templatePath}");
            return;
        }

        // Prepare data for the form
        DataTable dataTable = new DataTable();
        dataTable.Columns.Add("FirstName", typeof(string));
        DataRow row = dataTable.NewRow();
        row["FirstName"] = "John";
        dataTable.Rows.Add(row);

        // Create an interrupt monitor to allow cancellation
        using (InterruptMonitor monitor = new InterruptMonitor())
        {
            // Cancellation token that triggers after the timeout
            CancellationTokenSource cts = new CancellationTokenSource(TimeSpan.FromSeconds(timeoutSeconds));
            // When the token is cancelled, signal the monitor to interrupt the operation
            cts.Token.Register(() => monitor.Interrupt());

            Task fillTask = Task.Run(() =>
            {
                AutoFiller filler = new AutoFiller();
                filler.BindPdf(templatePath);
                filler.ImportDataTable(dataTable);
                filler.Save(outputPath);
            }, monitor.CancellationToken);

            try
            {
                fillTask.Wait(cts.Token);
                Console.WriteLine($"PDF filled and saved to '{outputPath}'.");
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Filling operation timed out and was cancelled.");
            }
            catch (AggregateException ae)
            {
                Console.WriteLine($"Error: {ae.Flatten().InnerException.Message}");
            }
        }
    }
}
