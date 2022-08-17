namespace VisualAcademy.Shared
{
    public interface IFileStorageManager
    {
        Task<string> UploadAsync(byte[] btes, string fileName, string folderPath, bool overwrite);
        Task<byte[]> DownloadAsync(string fileName, string folderPath);
        Task<bool> DeleteAsync(string fileName, string folderPath);
        string GetFolderPath(string ownerType, string ownerId, string fileType);
        string GetFolderPath(string ownerType, long ownerId, string fileType);
        string GetFolderPath(string ownerType, int ownerId, string fileType);

    }
}
