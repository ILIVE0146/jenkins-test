dotnet run --project ./testApp &
RETURN_OF_TEST=./tests/test.exe &
wait
exit $RETURN_OF_TEST