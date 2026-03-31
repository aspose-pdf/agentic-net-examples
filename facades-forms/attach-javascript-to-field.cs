using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (FormEditor editor = new FormEditor())
        {
            editor.BindPdf(inputPath);

            string script = "var qty = this.getField('Quantity').value; var price = this.getField('UnitPrice').value; this.getField('TotalPrice').value = qty * price;";

            bool added = editor.AddFieldScript("Quantity", script);
            if (!added)
            {
                Console.Error.WriteLine("Failed to add JavaScript to the Quantity field.");
            }

            editor.Save(outputPath);
        }

        Console.WriteLine($"JavaScript attached and saved to '{outputPath}'.");
    }
}
