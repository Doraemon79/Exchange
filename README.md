Use Frozendictionary. Although in this case Frozendictionary does not add much to the performances comapared to dictionary, it still makes sense to use an immutable container.
Rates are donloaded when the app is started.
Due to Frozendictionary being still a work in progress (there is no equivalent of IDictionary) use it as a property is not possible (I believe).
Values are in decimal, it is more precise and it is the way I did while working in a bank.
Reference currency is $.
Rates are fetched from https://freecurrencyapi.com/ the Get request is done everytime the app starts. I opened an account and used my personal key, I did not make a config.yams file I just hardcoded.



