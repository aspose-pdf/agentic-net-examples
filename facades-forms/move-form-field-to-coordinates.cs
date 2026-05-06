using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // source PDF containing the DateField
        const string outputPdf = "output.pdf";  // PDF after moving the field

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // FormEditor implements SaveableFacade and should be disposed via using
        using (FormEditor formEditor = new FormEditor(inputPdf, outputPdf))
        {
            // Move the field named "DateField" to (100,200) with width 150 and height 20
            // llx = 100, lly = 200, urx = 100 + 150 = 250, ury = 200 + 20 = 220
            bool moved = formEditor.MoveField("DateField", 100f, 200f, 250f, 220f);

            if (!moved)
            {
                Console.Error.WriteLine("Failed to move the field 'DateField'.");
            }
        }

        Console.WriteLine($"Field moved and saved to '{outputPdf}'.");
    }
}