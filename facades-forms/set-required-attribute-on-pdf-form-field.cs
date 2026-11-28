using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "survey_form.pdf";
        const string outputPath = "required_email.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // FormEditor is a facade for editing form structure; it does not implement IDisposable.
        var formEditor = new FormEditor();
        formEditor.BindPdf(inputPath);

        // Set the Required attribute on the field named "Email".
        // The correct API member is SetFieldAttribute, not SetFieldProperty.
        formEditor.SetFieldAttribute("Email", PropertyFlag.Required);

        // Save the modified PDF.
        formEditor.Save(outputPath);
        Console.WriteLine($"Saved PDF with required Email field to '{outputPath}'.");
    }
}
