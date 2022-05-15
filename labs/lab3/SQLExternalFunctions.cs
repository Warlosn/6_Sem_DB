using System;//my
using System.Linq;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;
using System.Text;
using System.IO;
using System.Data;

public class StoredProcedures
{
    [SqlProcedure]
    public static void GetOrdersBetweenDate(SqlDateTime beginning, SqlDateTime end)
    {
        SqlCommand command = new SqlCommand();
        command.Connection = new SqlConnection("Context connection = true");
        command.Connection.Open();
        const string sql = "select * from Orders where order_date between @beginning and @end";
        command.CommandText = sql;
        var param = command.Parameters.Add("@beginning", SqlDbType.DateTime);
        param.Value = beginning;
        param = command.Parameters.Add("@end", SqlDbType.DateTime);
        param.Value = end;
        SqlContext.Pipe?.ExecuteAndSend(command);
        command.Connection.Close();
    }
    [SqlFunction]
    public static SqlBoolean ReadTextFile(SqlString path, SqlString pathto)
    {
        try
        {
            if (!path.IsNull && !pathto.IsNull)
            {
                var dir = Path.GetDirectoryName(pathto.Value);
                if (!Directory.Exists(dir))
                    if (dir != null)
                        Directory.CreateDirectory(dir);
                using (var sr = new StreamReader(path.Value))
                {
                    string text = sr.ReadLine();
                    using (var sw = new StreamWriter(pathto.Value))
                    {
                        sw.WriteLine(text);
                    }
                    return SqlBoolean.True;
                }
            }
            return SqlBoolean.Null;
        }
        catch (Exception)
        {
            return SqlBoolean.Null;
        }
    }
}
[Serializable]
[SqlUserDefinedType(Format.UserDefined, MaxByteSize = 8000, Name = "UserData")]
public struct UserData : INullable, IBinarySerialize
{
    private bool _null;
    String adress;
    String phone;

    public override string ToString()
    {
        return $"{phone}, {adress}";
    }
    public bool IsNull
    {
        get
        {
            return _null;
        }
    }
    public static UserData Null
    {
        get
        {
            UserData h = new UserData();
            h._null = true;
            return h;
        }
    }
    public static UserData Parse(SqlString s)
    {
        string[] b = s.Value.Split(',');
        if (s.IsNull)
            return Null;

        UserData u = new UserData
        {
            phone = b[0],
            adress = b[1]
        };
        return u;
    }
    public void Read(BinaryReader r)
    {
        phone = r.ReadString();
    }

    public void Write(BinaryWriter w)
    {
        w.Write($"phone: {phone}, adress: {adress}");
    }
}