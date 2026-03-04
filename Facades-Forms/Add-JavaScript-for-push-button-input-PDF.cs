using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";      // source PDF containing the push button
        const string outputPdf = "output.pdf";     // PDF with JavaScript attached
        const string buttonName = "myButton";      // fully qualified name of the push button field
        const string jsCode = "app.alert('Button clicked!');";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Use FormEditor (a SaveableFacade) inside a using block for deterministic disposal
        using (FormEditor formEditor = new FormEditor())
        {
            // Bind the existing PDF document
            formEditor.BindPdf(inputPdf);

            // Add JavaScript to the specified push button field
            // AddFieldScript returns true on success; we ignore the return value here
            formEditor.AddFieldScript(buttonName, jsCode);

            // Save the modified PDF
            formEditor.Save(outputPdf);
        }

        Console.WriteLine($"JavaScript added to button '{buttonName}' and saved as '{outputPdf}'.");
    }
}