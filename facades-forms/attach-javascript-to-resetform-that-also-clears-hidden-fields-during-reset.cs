using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // JavaScript that resets the form and clears all hidden fields
        string js = @"
this.resetForm();
for (var i = 0; i < this.numFields; i++) {
    var f = this.getField(this.getFieldName(i));
    if (f.display == display.hidden) {
        f.value = '';
    }
}";

        // Load the PDF, attach the script to the button named "ResetForm", and save
        using (Document doc = new Document(inputPath))
        using (FormEditor editor = new FormEditor(doc))
        {
            editor.AddFieldScript("ResetForm", js);
            editor.Save(outputPath);
        }

        Console.WriteLine($"JavaScript attached and saved to '{outputPath}'.");
    }
}