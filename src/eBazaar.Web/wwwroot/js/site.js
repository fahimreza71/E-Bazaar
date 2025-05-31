let cart = {
    items: [],
    total: 0,
    userId: '3FA85F64-5717-4562-B3FC-2C963F66AFA6' // Default GUID for testing
};

const API_BASE_URL = 'http://localhost:5087';

// Load cart data from API
async function loadCart() {
    try {
        const response = await fetch(`${API_BASE_URL}/api/Cart/${cart.userId}`);
        if (response.ok) {
            const cartItems = await response.json();
            cart.items = cartItems;
            updateCartBadge();
        }
    } catch (error) {
        console.error('Error loading cart:', error);
    }
}

// Update cart badge count
async function updateCartBadge() {
    const cartCount = document.getElementById('cartItemCount');
    if (cartCount) {
        cartCount.textContent = cart.items.length;
    }
}

// Add to cart function (called from product page)
async function addToCart(productId) {
    try {
        const cartItem = {
            id: '00000000-0000-0000-0000-000000000000',
            userId: cart.userId,
            productId: productId,
            quantity: 1
        };

        //console.log('Sending cart item:', cartItem);

        const response = await fetch(`${API_BASE_URL}/api/Cart`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(cartItem)
        });

        if (response.ok) {
            await loadCart();
        } else {
            const errorText = await response.text();
            console.error('Error response:', errorText);
            throw new Error(errorText || 'Failed to add to cart');
        }
    } catch (error) {
        console.error('Error:', error);
        alert('Failed to add product to cart');
    }
}

// Update quantity in cart
async function updateQuantity(productId, change) {
    try {
        const cartItem = cart.items.find(item => item.productId == productId);
        console.log(cartItem);
        if (!cartItem) return;

        const newQuantity = cartItem.quantity + change;
        if (newQuantity <= 0) {
            await removeItem(productId);
            return;
        }

        const updatedCartItem = {
            id: cartItem.id,
            userId: cart.userId,
            productId: productId,
            quantity: newQuantity
        };

        const response = await fetch(`${API_BASE_URL}/api/Cart`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(updatedCartItem)
        });

        if (response.ok) {
            await loadCart();

            document.getElementById(`qty-${productId}`).textContent = newQuantity;
            const itemPrice = cartItem.product?.price || 0;
            document.getElementById(`total-${productId}`).textContent = (itemPrice * newQuantity).toFixed(2);
            let grandTotal = await updateGrandTotal(cart.userId);
            document.getElementById("grand-total").textContent = grandTotal;
        } else {
            throw new Error('Failed to update quantity');
        }
    } catch (error) {
        console.error('Error:', error);
        alert('Failed to update quantity');
    }
}

async function getCartItems(userId) {
    try {
        const response = await fetch(`${API_BASE_URL}/api/Cart/${userId}`);

        if (!response.ok) {
            throw new Error("Failed to load cart items");
        }

        const cartItems = await response.json();

        return cartItems;

    } catch (error) {
        console.error("Error fetching cart items:", error);
        alert("Failed to fetch cart items");
    }
}

async function updateGrandTotal(userId) {
    try {
        const cartItems = await getCartItems(userId);

        let total = 0;

        cartItems.forEach(item => {
            const quantity = item.quantity || 0;
            const price = item.product?.price || 0;
            total += quantity * price;
        });

        return total.toFixed(2);
    } catch (error) {
        console.error("Error updating grand total:", error);
        return "0.00";
    }
}

// Remove item from cart
async function removeItem(productId) {
    console.log("Remove iteam for id: " + productId)
    try {
        const response = await fetch(`${API_BASE_URL}/api/Cart/${cart.userId}/items/${productId}`, {
            method: 'DELETE',
        });

        if (response.ok) {
            await loadCart();
            let grandTotal = await updateGrandTotal(cart.userId);
            document.getElementById("grand-total").textContent = grandTotal;
        } else {
            throw new Error('Failed to remove item');
        }
    } catch (error) {
        console.error('Error:', error);
        alert('Failed to remove item');
    }
}

async function openCart() {
    await loadCart();
    document.getElementById('cart_modal').showModal();
}


// Initialize cart and modal on page load
document.addEventListener('DOMContentLoaded', () => {
    loadCart();
    // Initialize modal close buttons
    const modal = document.getElementById('cart_modal');
    if (modal) {
        const closeButtons = modal.querySelectorAll('button[onclick="cart_modal.close()"]');
        closeButtons.forEach(button => {
            button.onclick = () => modal.close();
        });
    }
});
