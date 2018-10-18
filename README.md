# dotnet-base64

This dotnet global tool can base64 encode/decode strings. 
It can be installed with the following command 
```
dotnet tool install -g dotnet-base64
```
It is invoked with the following command
```
base64 stringToEncode

base64 --decode stringToDecode
```
It also works with pipes in Powershell.
```
echo stringToEncode | base64
```
The primary reason I made this tool is to be able to easily decode Kubernetes secrets like the following.
```
kubectl get secret default-token -o jsonpath="{.data.token}" | base64 --decode
```
