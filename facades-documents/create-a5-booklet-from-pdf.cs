using System;
using System.IO;
using System.Reflection;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string sourcePath = "source.pdf";
        const string outputPath = "booklet_a5.pdf";

        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePath}");
            return;
        }

        // Load the BookletSettings type via reflection – it may be missing in older Aspose.Pdf versions.
        Type bookletSettingsType = Type.GetType("Aspose.Pdf.Facades.BookletSettings, Aspose.Pdf");
        if (bookletSettingsType == null)
        {
            Console.Error.WriteLine("BookletSettings type not found in the loaded Aspose.Pdf assembly.");
            return;
        }

        // Create an instance of BookletSettings and set the custom A5 page size (≈419 × 595 points).
        object bookletSettings = Activator.CreateInstance(bookletSettingsType);
        PropertyInfo pageSizeProp = bookletSettingsType.GetProperty("PageSize");
        if (pageSizeProp != null && pageSizeProp.CanWrite)
        {
            var a5Size = new PageSize(419, 595); // width, height in points (1 pt = 1/72 inch)
            pageSizeProp.SetValue(bookletSettings, a5Size);
        }
        else
        {
            Console.Error.WriteLine("PageSize property not found on BookletSettings.");
            return;
        }

        // PdfFileEditor does NOT implement IDisposable, so we instantiate it directly.
        PdfFileEditor editor = new PdfFileEditor();

        // Locate the overload CreateBooklet(string source, string destination, BookletSettings settings).
        MethodInfo createBookletMethod = typeof(PdfFileEditor).GetMethod(
            "CreateBooklet",
            new[] { typeof(string), typeof(string), bookletSettingsType });

        if (createBookletMethod == null)
        {
            Console.Error.WriteLine("CreateBooklet overload that accepts BookletSettings was not found.");
            return;
        }

        // Invoke the overload via reflection.
        createBookletMethod.Invoke(editor, new object[] { sourcePath, outputPath, bookletSettings });

        Console.WriteLine($"Booklet created with A5 pages: {outputPath}");
    }
}
