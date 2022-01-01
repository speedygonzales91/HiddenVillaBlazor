redirectToCheckout = function (sessionId) {
    var stipe = Stripe('pk_test_51KCk3dENfuFdQ4VZDX9qWDYoJfMFfVOg8CghLcPQQROhbrzcMYxvOyReWKHIzZcYP3afMY9zDsHiCmR9YekYizsj00uT2y3kaa');
    stripe.redirectToCheckout({
        sessionId: sessionId
    });
}