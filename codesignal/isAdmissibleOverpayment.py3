def solution(prices, notes, x):
    from functools import reduce
    def deltaPrice(prev, pack):
        price, note = pack
        keys = note.split()
        result = 0
        if keys[1] == 'higher': result = price - price * 100 / (100 + float(keys[0][:-1]))
        elif keys[1] == 'lower': result = price - price * 100 / (100 - float(keys[0][:-1]))
        return prev + result
    return x >= reduce(deltaPrice, zip(prices, notes), 0)
