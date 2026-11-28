using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output2.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Bind the PDF to FormEditor and add a ListBox field
        using (FormEditor formEditor = new FormEditor())
        {
            formEditor.BindPdf(inputPath);

            // Add ListBox named "CountryList" on page 2 with rectangle (50,400)-(150,450)
            formEditor.AddField(FieldType.ListBox, "CountryList", 2, 50, 400, 150, 450);

            // Populate the ListBox with items
            string[] countries = { "USA", "Canada", "Mexico", "UK", "Germany" };
            foreach (var c in countries)
            {
                formEditor.AddListItem("CountryList", c);
            }

            // Save the modified PDF
            formEditor.Save(outputPath);
        }

        Console.WriteLine($"Saved output to '{outputPath}'.");
    }
}
