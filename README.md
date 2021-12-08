  # Pizza chatbot


This is an assignment for PROG8050 at Conestoga College.

To run:

```
dotnet run --project OrderBotPage

```

To test:

```
dotnet test
```
## To modify: 

if you're a collaborator, make a new branch and make a pull request: 

  Please ensure your PR is made to this repository and not the repository which this is a fork of. If you PR that repository it will not be mergable here.

if you are not a collaborator: fork and then make a pr

## Use

Currently, the chatbot uses a parser to interpret messages. As of version 1, the parser has the following known limitations.

1. It cannot handle written-out numbers (you must use 1 instead of one)

2. It cannot handle multiples of a custom pizza, only of standard pizzas

The parser handles best messages like "1 medium pizza with mushrooms and onions" or "1 ham and cheddar pizza". You can add toppings to custom pizzas like "1 hawaiian pizza with onions", and can remove toppings from custom pizzas with "1 hawaiian pizza without pineapple".

The help functionality is implemented, but is not super user friendly - that needs some work.


## Todo

The parser needs some touch-ups, but those are low priority at the moment.

