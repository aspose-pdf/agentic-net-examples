using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    private const string UnicodeNickname = "测试昵称😊";

    private static string CreateEmptyPdf()
    {
        string tempPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".pdf");
        using (var doc = new Document())
        {
            doc.Save(tempPath);
        }
        return tempPath;
    }

    private static void TestNicknameStoreUnicode()
    {
        string pdfPath = CreateEmptyPdf();
        try
        {
            // Store the nickname in the Title field (Unicode supported)
            using (var doc = new Document(pdfPath))
            {
                doc.Info.Title = UnicodeNickname;
                doc.Save(pdfPath);
            }

            // Reload and verify the stored value
            using (var doc = new Document(pdfPath))
            {
                bool pass = doc.Info.Title == UnicodeNickname;
                Console.WriteLine(pass ? "PASS Nickname_ShouldStoreUnicodeCharacters" : "FAIL Nickname_ShouldStoreUnicodeCharacters");
            }
        }
        finally
        {
            if (File.Exists(pdfPath))
                File.Delete(pdfPath);
        }
    }

    private static void TestNicknamePersistMultipleSaves()
    {
        string pdfPath = CreateEmptyPdf();
        try
        {
            // First save – set the nickname (Title)
            using (var doc = new Document(pdfPath))
            {
                doc.Info.Title = UnicodeNickname;
                doc.Save(pdfPath);
            }

            // Second save – modify another metadata field (Author)
            using (var doc = new Document(pdfPath))
            {
                doc.Info.Author = "Sample Author";
                doc.Save(pdfPath);
            }

            // Reload and verify both fields are persisted
            using (var doc = new Document(pdfPath))
            {
                bool pass = doc.Info.Title == UnicodeNickname && doc.Info.Author == "Sample Author";
                Console.WriteLine(pass ? "PASS Nickname_ShouldPersistAfterMultipleSaves" : "FAIL Nickname_ShouldPersistAfterMultipleSaves");
            }
        }
        finally
        {
            if (File.Exists(pdfPath))
                File.Delete(pdfPath);
        }
    }

    static void Main()
    {
        TestNicknameStoreUnicode();
        TestNicknamePersistMultipleSaves();
    }
}