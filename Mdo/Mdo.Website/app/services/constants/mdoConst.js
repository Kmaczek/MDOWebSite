function MdoBoot()
{
    var data = {};
    var initialized = false;

    // CONSTANTS
    data.username = 'none';
    data.apiUrl = 'http://localhost:15555/';
    data.version = '0.0.1';


    this.loadData = function(initData)
    {
        if (initialized)
        {
            return;
        }

        data = initData;
        initialized = true;
    }

    this.getData = function()
    {
        if (!initialized)
        {
            console.info('MdoBoot -> loadData wasnt run');
        }

        return data;
    }
}