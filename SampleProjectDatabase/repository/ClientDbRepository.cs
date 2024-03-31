using log4net;
using SampleProjectDatabase.model;

namespace SampleProjectDatabase.repository;

public class ClientDbRepository : IClientRepository
{
    private readonly ILog _logger = LogManager.GetLogger(typeof(ClientDbRepository));
    
    public ClientDbRepository()
    {
        _logger.Info("I have reached the constructor!");
        
    }
    
    public Client Save(Client entity)
    {
        var dbConnection = DbConnection.GetConnection();
        using (var command = dbConnection.CreateCommand())
        {
            command.CommandText = "insert into Client(nume) values (@name)";
            
            var parameter = command.CreateParameter();
            parameter.ParameterName = "@name";
            parameter.Value = entity.Name;
            command.Parameters.Add(parameter);

            command.ExecuteNonQuery();
            
            // o data salvat i s-a pus un id automat de catre db. Fie facem un select dupa nume ca sa 
            // gasim id-ul fie gasim alte metode. 
            var clientId = GetClientIdByName(entity.Name);
            entity.Id = clientId;
            return entity;
        }
    }

    private long GetClientIdByName(string name)
    {
        var dbConnection = DbConnection.GetConnection();
        using (var command = dbConnection.CreateCommand())
        {
            command.CommandText = "select id from Client where nume = @name";
            
            var parameter = command.CreateParameter();
            parameter.ParameterName = "@name";
            parameter.Value = name;
            command.Parameters.Add(parameter);
            
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var idClient = reader.GetInt32(0);
                    return idClient;
                }
            }
        }
        throw new Exception("Nu am gasit un client cu acest nume! nume = " + name);
    }

    public bool Delete(long id)
    {
        throw new NotImplementedException();
    }

    public Client Update(Client entity)
    {
        throw new NotImplementedException();
    }

    public List<Client> FindAll()
    {
        List<Client> clientList = [];
        var dbConnection = DbConnection.GetConnection();
        using (var command = dbConnection.CreateCommand())
        {
            command.CommandText = "select * from Client";
            
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var idClient = reader.GetInt32(0);
                    var nume = reader.GetString(1);
                    
                    var client = new Client(nume);
                    client.Id = idClient;
                    clientList.Add(client);
                }
            }
        }
        return clientList;
    }

    public Client FindById(long id)
    {
        throw new NotImplementedException();
    }
}