tfs-cli
=======

Team Foundation Server CLI tools that can execute command individually or start a Daemon that can accept HTTP requests.  


##Command Line Usage##

Each of these commands will return a JSON representation of the object to the Command Line.  The following are the options. Each action requires a subset of options.

```
TfsCli 1.0.0.0
Copyright c  2014

  -u, --uri           Required. TFS Server URI that will be connected to

  -a, --action        Action to be taken. Each action can use custom options.

  -c, --collection    TFS Collection to query against

  -p, --project       Project inside of a collection to use

  -b, --build         Build definition name to search for. Accepts wildcards

  -d, --Daemon        Start as a daemon that will accept entity requests

  --help              Display this help screen.
```

###Build Definitions
You can query build definitions by using `--action builddefs`. This action requires `--uri`, `--collection`, and `--project`. You can optionally provide `--build` for a match on the name. The `--build` argument can be a partial match or exact match.

```
# All build definitions for the project
TfsCli.exe -a builddefs -u TFS_SERVER_PATH -c COLLECITON_NAME -p PROJECT_NAME

# Build definitions matching a name
TfsCli.exe -a builddefs -u TFS_SERVER_PATH -c COLLECITON_NAME -p PROJECT_NAME -b STARTS_WITH
TfsCli.exe -a builddefs -u TFS_SERVER_PATH -c COLLECITON_NAME -p PROJECT_NAME -b EXACT_NAME
```

### Builds for Build Definition
You can query for the builds by using `--action builds`. This action requires `--uri`, `--collection`, `--project`, and `--build`.
```
# All builds for a build definition
TfsCli.exe -a builds -u TFS_SERVER_PATH -c COLLECITON_NAME -p PROJECT_NAME -b BUILD_DEF_NAME
```

### Last Successful Build
You can query the last successful build by using `--action builds-last`. This action requires `--uri`, `--collection`, `--project`, and `--build`.
```
# Last successful build for a build definition
TfsCli.exe -a builds-last -u TFS_SERVER_PATH -c COLLECITON_NAME -p PROJECT_NAME -b ACTUAL_NAME
```

##Daemon
You can run the is a Daemon that will spawn a web server that runs on `localhost:3456`.  You can then query this path to retreive JSON data the is provided by the command line.  This options requires supplying `--uri` and `--daemon`.

```
TfsCli.exe -u TFS_SERVER_PATH -d
```

Once the service is running, you can make web requests to it:
```
# All build definitions for a project
http://localhost:3456/api/{collection}/{project}/builddefinitions

# All build definitions matching a name
http://localhost:3456/api/{collection}/{project}/builddefinitions?name=start_with

# All builds for a build definition
http://localhost:3456/api/{collection}/{project}/{buildDefinition}/builds

# Latest build for a build definition
http://localhost:3456/api/{collection}/{project}/{buildDefinition}/build?latest=true
```
