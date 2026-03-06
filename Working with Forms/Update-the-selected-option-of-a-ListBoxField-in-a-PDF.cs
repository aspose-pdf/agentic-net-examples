using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the source PDF and the output PDF
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        // Name of the ListBox field whose selected option we want to change
        const string listBoxFieldName = "MyListBox";

        // Desired selected index (1‑based). For example, 2 selects the second item.
        const int selectedIndex = 2;

        // Verify that the source file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Open the PDF with the Form facade, modify the field, and save the result.
        // The Form class implements IDisposable, so we wrap it in a using block.
        using (Form form = new Form(inputPdf))
        {
            // FillField(string, int) sets the selected item of a ListBox/ComboBox/Radio field.
            // The index is 1‑based; 0 means no selection.
            bool success = form.FillField(listBoxFieldName, selectedIndex);

            if (!success)
            {
                Console.Error.WriteLine($"Failed to set selected item for field '{listBoxFieldName}'.");
                return;
            }

            // Save the modified PDF to a new file.
            form.Save(outputPdf);
        }

        Console.WriteLine($"Selected option updated and saved to '{outputPdf}'.");
    }
}